using BittleBattleBaseball.Models.DomainModels;
using BittleBattleBaseball.Models.DTOs;
using BittleBattleBaseball.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BittleBattleBaseball.ApplicationService
{
    public class PlayerApplicationService
    {
        private static List<PlayerImageSearchResult> _playerImageCache;

        public bool HasChanges { get; set; }

        public static List<PlayerImageSearchResult> PlayerImageCache
        {
            get
            {
                if(_playerImageCache == null)
                {
                    _playerImageCache = new List<PlayerImageSearchResult>();

                    //TODO - Load from a .json file from disk
                    LoadJson();
                }

                return _playerImageCache;
            }
        }

        public static void LoadJson()
        {
            if (File.Exists(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\jsonPlayerDictionary.json"))
            {
                using (StreamReader r = new StreamReader(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\jsonPlayerDictionary.json"))
                {
                    string json = r.ReadToEnd();
                    _playerImageCache = JsonConvert.DeserializeObject<IEnumerable<PlayerImageSearchResult>>(json).ToList();
                }
            }
        }

        public static void WriteChangesToFile()
        {
            string json = JsonConvert.SerializeObject(PlayerImageCache, Formatting.None);

            using (StreamWriter file = new StreamWriter(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\jsonPlayerDictionary.json", false))
            {
                file.Write(json);
            }
        }

        #region Player Images

        public PlayerImageSearchResult GetPlayerImage(string playerName)
        {
            var cachedPlayerImage = PlayerImageCache.FirstOrDefault(x => x.PlayerName == playerName);
            if (cachedPlayerImage != null)
            {
                return new PlayerImageSearchResult { ImageURL = HttpUtility.UrlDecode(@cachedPlayerImage.ImageURL)  };
            }

            string jsonData = this.GetPlayerImagesJson(playerName);

            try
            {
                GetPlayerImagesDTO dto = GetPlayerImagesDTO.FromJson(jsonData);
                var result = GetPlayerImagesFromDTO(dto);
                PlayerImageCache.Add(new PlayerImageSearchResult { PlayerName = playerName, ImageURL = HttpUtility.UrlEncode(@result.ImageURL) });
                this.HasChanges = true;
                return result;
            }
            catch
            {
                return null;
            }
        }

        private static PlayerImageSearchResult GetPlayerImagesFromDTO(GetPlayerImagesDTO dto)
        {           
            if (dto != null && dto.results != null && dto.results.Any())
            {
                foreach (var dtoResult in dto.results)
                {
                    if (!string.IsNullOrEmpty(dtoResult.iurl) 
                        && (
                        dtoResult.iurl.ToLower().Contains("espncdn.com") && dtoResult.iurl.ToLower().Contains("headshots/mlb")
                         ||
                        (!string.IsNullOrWhiteSpace(dtoResult.domain) && 
                        (dtoResult.domain.ToLower().Contains("baseball")
                        || dtoResult.domain.ToLower().Contains("baseballhall.org")
                        || dtoResult.domain.ToLower().Contains("cloudfront.net"))))
                       )
                    {
                        return new PlayerImageSearchResult
                        {
                            ImageURL = dtoResult.iurl,
                            ImageSourceDomain = dtoResult.domain
                        };
                    }
                }
            }

            return null;
        }

        private string GetPlayerImagesJson(string playerName)
        {
            System.Threading.Thread.Sleep(1100);

            string url = $"https://faroo-faroo-web-search.p.rapidapi.com/api?q='{playerName} espn'";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Headers.Add("X-RapidAPI-Key", "af5352e3e5msh027e7a5c8c8cc76p157788jsndab27210c9c4");

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }

        #endregion

        #region Hitting
        public HitterPlayerSeasonViewModel GetPlayerSeasonHittingStats(int season, int playerId, string leagueListId, string gameType)
        {
            string jsonData = this.GetSeasonHittingStatsJson(season, playerId, leagueListId, gameType);

            try
            {
                GetPlayerSeasonHittingSingleTeamStatsDTO dto = GetPlayerSeasonHittingSingleTeamStatsDTO.FromJson(jsonData);
                return GetPlayerSeasonHittingSingleTeamViewModelFromDTO(dto);
            }
            catch(JsonSerializationException)
            {
                try
                {
                    GetPlayerSeasonHittingMultiTeamStatsDTO dto = GetPlayerSeasonHittingMultiTeamStatsDTO.FromJson(jsonData);
                    return GetPlayerSeasonHittingMultiTeamViewModelFromDTO(dto);
                }
                catch
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        private string GetSeasonHittingStatsJson(int season, int playerId, string leagueListId, string gameType)
        {
            string url = $"https://mlb-data.p.rapidapi.com/json/named.player_teams.bam?season=\'{season}\'&player_id=\'{playerId}\'&league_list_id=\'{leagueListId}\'&game_type=\'{gameType}\'";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Headers.Add("X-RapidAPI-Key", "af5352e3e5msh027e7a5c8c8cc76p157788jsndab27210c9c4");
            request.Headers.Add("x-rapidapi-host", "mlb-data.p.rapidapi.com");

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }

        private static HitterPlayerSeasonViewModel GetPlayerSeasonHittingMultiTeamViewModelFromDTO(GetPlayerSeasonHittingMultiTeamStatsDTO dto)
        {
            HitterPlayerSeasonViewModel returnVal = new HitterPlayerSeasonViewModel();
            
            if(dto != null && dto.sport_hitting_tm != null && dto.sport_hitting_tm.queryResults != null && dto.sport_hitting_tm.queryResults.row != null)
            {
                foreach (Stat playerStats in dto.sport_hitting_tm.queryResults.row)
                {
                    PopulateBattingStats(returnVal, playerStats);
                }
            }

            return returnVal;
        }

        private static HitterPlayerSeasonViewModel GetPlayerSeasonHittingSingleTeamViewModelFromDTO(GetPlayerSeasonHittingSingleTeamStatsDTO dto)
        {
            HitterPlayerSeasonViewModel returnVal = new HitterPlayerSeasonViewModel();

            if (dto != null && dto.sport_hitting_tm != null && dto.sport_hitting_tm.queryResults != null && dto.sport_hitting_tm.queryResults.row != null)
            {
                var playerStats = dto.sport_hitting_tm.queryResults.row;
                PopulateBattingStats(returnVal, playerStats);
            }

            return returnVal;
        }

        private static void PopulateBattingStats(HitterPlayerSeasonViewModel hitterInfo, Stat playerStats)
        {
            hitterInfo.AB = !playerStats.ab.Contains("-") ? Convert.ToInt32(playerStats.ab) : 0;
            hitterInfo.AVG = !playerStats.avg.Contains("-") ? Convert.ToDecimal( playerStats.avg) : 0;
            hitterInfo.OBP = !playerStats.obp.Contains("-") ? Convert.ToDecimal(playerStats.obp) : 0;
            hitterInfo.SLG = !playerStats.slg.Contains("-") ? Convert.ToDecimal(playerStats.slg) : 0;
            hitterInfo.HR = !playerStats.hr.Contains("-") ? Convert.ToInt32(playerStats.hr) : 0;
            hitterInfo.RBI = !playerStats.rbi.Contains("-") ? Convert.ToInt32(playerStats.rbi) : 0;
            hitterInfo.BB = !playerStats.bb.Contains("-") ? Convert.ToInt32(playerStats.bb) : 0;

           

            if(!string.IsNullOrWhiteSpace(playerStats.sb))
                hitterInfo.SB = !playerStats.sb.Contains("-") ? Convert.ToInt32(playerStats.sb) : 0;

            if (!string.IsNullOrWhiteSpace(playerStats.cs))
                hitterInfo.CS = !playerStats.cs.Contains("-") ? Convert.ToInt32(playerStats.cs) : 0;

            if (!string.IsNullOrWhiteSpace(playerStats.gidp))
                hitterInfo.GIDP = !playerStats.gidp.Contains("-") ? Convert.ToInt32(playerStats.gidp) : 0;

            hitterInfo.H = !playerStats.h.Contains("-") ? Convert.ToInt32(playerStats.h) : 0;
            hitterInfo.R = !playerStats.r.Contains("-") ? Convert.ToInt32(playerStats.r) : 0;

            if (!string.IsNullOrWhiteSpace(playerStats.so))
                hitterInfo.SO = !playerStats.so.Contains("-") ? Convert.ToInt32(playerStats.so) : 0;

            if (!string.IsNullOrWhiteSpace(playerStats.xbh))
                hitterInfo.XBH = !playerStats.xbh.Contains("-") ? Convert.ToInt32(playerStats.xbh) : 0;

            hitterInfo.Player = new PlayerViewModel
            {
                Id = Convert.ToInt32(playerStats.player_id)
            };
        }

        #endregion

        #region Pitching

        public PitcherPlayerSeasonViewModel GetPlayerSeasonPitchingStats(int season, int playerId, string leagueListId, string gameType)
        {
            string jsonData = this.GetSeasonPitchingStatsJson(season, playerId, leagueListId, gameType);

            try
            {
                GetPlayerSeasonPitchingSingleTeamStatsDTO dto = GetPlayerSeasonPitchingSingleTeamStatsDTO.FromJson(jsonData);
                return GetPlayerSeasonPitchingSingleTeamViewModelFromDTO(dto);
            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                try
                {
                    GetPlayerSeasonPitchingMultiTeamStatsDTO dto = GetPlayerSeasonPitchingMultiTeamStatsDTO.FromJson(jsonData);
                    return GetPlayerSeasonPitchingMultiTeamViewModelFromDTO(dto);
                }
                catch
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private string GetSeasonPitchingStatsJson(int season, int playerId, string leagueListId, string gameType)
        {
            string url = $"https://mlb-data.p.rapidapi.com/json/named.sport_pitching_tm.bam?season=\'{season}\'&player_id=\'{playerId}\'&league_list_id=\'{leagueListId}\'&game_type=\'{gameType}\'";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Headers.Add("X-RapidAPI-Key", "af5352e3e5msh027e7a5c8c8cc76p157788jsndab27210c9c4");

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }

        private static PitcherPlayerSeasonViewModel GetPlayerSeasonPitchingSingleTeamViewModelFromDTO(GetPlayerSeasonPitchingSingleTeamStatsDTO dto)
        {
            PitcherPlayerSeasonViewModel returnVal = new PitcherPlayerSeasonViewModel();

            if (dto != null && dto.sport_pitching_tm != null && dto.sport_pitching_tm.queryResults != null && dto.sport_pitching_tm.queryResults.row != null)
            {
                var playerStats = dto.sport_pitching_tm.queryResults.row;
                PopulatePitchingStats(returnVal, playerStats);
            }

            return returnVal;
        }

        private static PitcherPlayerSeasonViewModel GetPlayerSeasonPitchingMultiTeamViewModelFromDTO(GetPlayerSeasonPitchingMultiTeamStatsDTO dto)
        {
            PitcherPlayerSeasonViewModel returnVal = new PitcherPlayerSeasonViewModel();

            if (dto != null && dto.sport_pitching_tm != null && dto.sport_pitching_tm.queryResults != null && dto.sport_pitching_tm.queryResults.row != null)
            {
                foreach (PitchingStat playerStats in dto.sport_pitching_tm.queryResults.row)
                {
                    PopulatePitchingStats(returnVal, playerStats);
                }
            }

            return returnVal;
        }

        private static void PopulatePitchingStats(PitcherPlayerSeasonViewModel returnVal, PitchingStat playerStats)
        {
            returnVal.Wins = !playerStats.w.Contains("-") ? Convert.ToInt32(playerStats.w) : 0;
            returnVal.Losses = !playerStats.l.Contains("-") ? Convert.ToInt32(playerStats.l) : 0;
            returnVal.ERA = !playerStats.era.Contains("-") ? Convert.ToDecimal(playerStats.era) : 0;
            returnVal.WHIP = !playerStats.whip.Contains("*") ? Convert.ToDecimal(playerStats.whip) : 0;
         

            returnVal.Player = new PlayerViewModel
            {
                Id = Convert.ToInt32(playerStats.player_id)
            };
        }


        #endregion
    }
}
