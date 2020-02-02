using BittleBattleBaseball.Models.DTOs;
using BittleBattleBaseball.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace BittleBattleBaseball.ApplicationService
{
    public class TeamApplicationService
    {
       // private const string RAPID_API_GET_TEAMS_BY_SEASON_ENDPOINT_URL = "https://mlb-data.p.rapidapi.com/json/named.team_all_season.bam";

      public  List<TeamSearchResultViewModel> GetTeamsBySeason(int season)
        {
            string jsonData = this.GetTeamsBySeasonJson(season);

            GetTeamsBySeasonDTO teamsBySeasonDto = GetTeamsBySeasonDTO.FromJson(jsonData);

            return GetTeamsBySeasonViewModelFromDTO(season, teamsBySeasonDto);
        }

        public RosterSearchResultViewModel GetRosterBySeason(int season, int teamId)
        {
            string jsonData = this.GetRosterBySeasonJson(season, teamId);

            GetRosterBySeasonDTO getRosterBySeasonDTO = GetRosterBySeasonDTO.FromJson(jsonData);

            return GetRosterBySeasonViewModelFromDTO(season, teamId, getRosterBySeasonDTO);
        }

        private static List<TeamSearchResultViewModel> GetTeamsBySeasonViewModelFromDTO(int season, GetTeamsBySeasonDTO teamsBySeasonDto)
        {
            List<TeamSearchResultViewModel> returnList = new List<TeamSearchResultViewModel>();

            if(teamsBySeasonDto != null && teamsBySeasonDto.team_all_season != null 
                && teamsBySeasonDto.team_all_season.queryResults != null && teamsBySeasonDto.team_all_season.queryResults.row != null)
            {
                foreach(TeamSearchResult teamResult in teamsBySeasonDto.team_all_season.queryResults.row)
                {
                    if (!string.IsNullOrEmpty(teamResult.sport_code) && teamResult.sport_code.ToLower() == "mlb" 
                        && !string.IsNullOrWhiteSpace(teamResult.venue_name)
                        && teamResult.name_display_full.ToLower() != "to be determined"
                        && teamResult.name_display_full.ToLower() != "office of the commissioner"
                        && !teamResult.name_display_full.ToLower().Contains("all-star")
                        && teamResult.venue_name.ToLower() != "al city" && teamResult.venue_name.ToLower() != "nl city" && teamResult.venue_name.ToLower() != "tbd")
                    {
                        returnList.Add(new TeamSearchResultViewModel
                        {
                            Id = Convert.ToInt32(teamResult.team_id),
                            TeamName = teamResult.name_display_full,
                            League = teamResult.league,
                            Season = season,
                            Ballpark = teamResult.venue_name,
                            Name = teamResult.name,
                            City = teamResult.city,
                            NameAbbrev = teamResult.name_abbrev,
                            LogoUrl = "https://d2p3bygnnzw9w3.cloudfront.net/req/202001161/tlogo/br/" + teamResult.name_abbrev + "-" + season + ".png"
                        });
                    }
                    else
                    {
                        Debug.WriteLine(teamResult.ToString());
                    }
                }               
            }

            return returnList.OrderByDescending(x => x.League).ToList();
        }

        private static RosterSearchResultViewModel GetRosterBySeasonViewModelFromDTO(int season, int teamId, GetRosterBySeasonDTO dto)
        {
            RosterSearchResultViewModel returnVal = new RosterSearchResultViewModel { Id = teamId,  Season = season};

            if (dto != null && dto.roster_team_alltime != null && dto.roster_team_alltime.queryResults != null 
                && dto.roster_team_alltime.queryResults.row != null && dto.roster_team_alltime.queryResults.row.Any())
            {
                returnVal.Hitters = new List<HitterPlayerSeasonViewModel>();
                returnVal.Pitchers = new List<PitcherPlayerSeasonViewModel>();
                PlayerApplicationService playerService = new PlayerApplicationService();
                
                foreach (var rosterPlayerResult in dto.roster_team_alltime.queryResults.row)
                {
                    var playerVm = new PlayerViewModel
                    {
                        Id = Convert.ToInt32(rosterPlayerResult.player_id),
                        Bats = rosterPlayerResult.bats,
                        Throws = rosterPlayerResult.throws,
                        PlayerName = rosterPlayerResult.name_first_last,
                        DOB = rosterPlayerResult.birth_date,
                        Position = rosterPlayerResult.primary_position,
                       // Weight = Convert.ToInt32(rosterPlayerResult.weight),
                       // Height = Convert.ToDecimal(rosterPlayerResult.height_feet + "." + rosterPlayerResult.height_inches)
                    };

                    //if (season > 1995)
                    //{
                    //    var imageSearchResult = playerService.GetPlayerImage(playerVm.PlayerName);
                    //    if (imageSearchResult != null)
                    //    {
                    //        playerVm.PlayerImageURL = imageSearchResult.ImageURL;// HttpUtility.UrlDecode();
                    //    }
                    //    else
                    //    {


                    //        if (rosterPlayerResult.primary_position.ToLower().Trim().Contains("p"))
                    //            playerVm.PlayerImageURL = "../assets/images/emptyPitcherIcon.png";
                    //        else
                    //            playerVm.PlayerImageURL = "../assets/images/emptyHitterIcon.png";
                    //    }
                    //}
                    //else
                    //{
                    //    if (rosterPlayerResult.primary_position.ToLower().Trim().Contains("p"))
                    //        playerVm.PlayerImageURL = "../assets/images/emptyPitcherIcon.png";
                    //    else
                    //        playerVm.PlayerImageURL = "../assets/images/emptyHitterIcon.png";
                    //}

                    playerVm.PlayerImageURL = "https://securea.mlb.com/mlb/images/players/head_shot/" + playerVm.Id + ".jpg";

                    if (rosterPlayerResult.primary_position.ToLower().Trim().Contains("p"))
                    {
                       
                        var playerSeasonVm = playerService.GetPlayerSeasonPitchingStats(season, playerVm.Id, "mlb", "R");
                        if (playerSeasonVm != null)
                        {
                            playerSeasonVm.GameType = "R";
                            playerSeasonVm.LeagueType = "mlb";
                            playerSeasonVm.Player = playerVm;
                            playerSeasonVm.Season = season;
                            returnVal.Pitchers.Add(playerSeasonVm);
                        }
                    }
                    else
                    {
                        HitterPlayerSeasonViewModel playerSeasonVm = playerService.GetPlayerSeasonHittingStats(season, playerVm.Id, "mlb", "R");
                        
                        if (playerSeasonVm != null)
                        {
                            playerSeasonVm.GameType = "R";
                            playerSeasonVm.LeagueType = "mlb";
                            playerSeasonVm.Player = playerVm;
                            playerSeasonVm.Season = season;
                            returnVal.Hitters.Add(playerSeasonVm);
                        }
                    }

                  
                }

                if (playerService.HasChanges)
                {
                    PlayerApplicationService.WriteChangesToFile();
                }
            }

            return returnVal;
        }

       

        private string GetTeamsBySeasonJson(int season)
        {
            string url = $"https://mlb-data.p.rapidapi.com/json/named.team_all_season.bam?season=\'{season}\'";
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

        private string GetRosterBySeasonJson(int season, int teamId)
        {
            string url = $"https://mlb-data.p.rapidapi.com/json/named.roster_team_alltime.bam?team_id=\'{teamId}\'&start_season=\'{season}\'&end_season=\'{season}\'";
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
    }
}
