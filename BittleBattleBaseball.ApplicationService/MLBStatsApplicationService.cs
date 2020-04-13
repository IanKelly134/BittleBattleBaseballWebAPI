using BittleBattleBaseball.Models.DTOs;
using BittleBattleBaseball.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BittleBattleBaseball.ApplicationService
{
    public class MLBStatsApplicationService
    {
        private static List<MLBYearByYearBattingStatsDTO> _mLBYearByYearBattingStatsCache;
        private static List<MLBYearByYearPitchingStatsDTO> _mLBYearByYearPitchingStatsCache;

        public MLBStatsApplicationService()
        {
            LoadBattingJson();
            LoadPitchingJson();
        }


        public MLBYearByYearBattingStatsViewModel GetLeagueBattingStatsByYear(int season)
        {
            MLBYearByYearBattingStatsDTO seasonStat = MLBStatsApplicationService.MLBYearByYearBattingStatsCache.FirstOrDefault(x => x.Year == season);
            if(seasonStat != null)
            {
                return this.ConvertDTOToViewModel(seasonStat);
            }

            throw new System.Exception("Unable to load MLB Hitting Stats for season " + season);

        }

        private MLBYearByYearBattingStatsViewModel ConvertDTOToViewModel(MLBYearByYearBattingStatsDTO seasonStat)
        {
            return new MLBYearByYearBattingStatsViewModel
            {
                OBP = Convert.ToDecimal(seasonStat.OBP),
                SLG = Convert.ToDecimal(seasonStat.SLG),
                Year = seasonStat.Year
            };
        }

        public MLBYearByYearPitchingStatsViewModel GetLeaguePitchingStatsByYear(int season)
        {
            return new MLBYearByYearPitchingStatsViewModel();

            MLBYearByYearPitchingStatsDTO seasonStat = MLBStatsApplicationService.MLBYearByYearPitchingStatsCache.FirstOrDefault(x => x.Year == season);
            if (seasonStat != null)
            {
                return this.ConvertDTOToViewModel(seasonStat);
            }

            throw new System.Exception("Unable to load MLB Pitching Stats for season " + season);

        }

        private MLBYearByYearPitchingStatsViewModel ConvertDTOToViewModel(MLBYearByYearPitchingStatsDTO seasonStat)
        {
            return new MLBYearByYearPitchingStatsViewModel
            {
                WHIP = Convert.ToDecimal(seasonStat.WHIP),
                Year = seasonStat.Year
            };
        }

        private static List<MLBYearByYearBattingStatsDTO> MLBYearByYearBattingStatsCache
        {
            get
            {
                if (_mLBYearByYearBattingStatsCache == null)
                {
                    _mLBYearByYearBattingStatsCache = new List<MLBYearByYearBattingStatsDTO>();

                    //TODO - Load from a .json file from disk
                    LoadBattingJson();
                }

                return _mLBYearByYearBattingStatsCache;
            }
        }

        private static List<MLBYearByYearPitchingStatsDTO> MLBYearByYearPitchingStatsCache
        {
            get
            {
                if (_mLBYearByYearPitchingStatsCache == null)
                {
                    _mLBYearByYearPitchingStatsCache = new List<MLBYearByYearPitchingStatsDTO>();

                    //TODO - Load from a .json file from disk
                    LoadPitchingJson();
                }

                return _mLBYearByYearPitchingStatsCache;
            }
        }

        private static void LoadBattingJson()
        {
        

            //var currDir = System.IO.Directory.GetCurrentDirectory();
            string envCurrDir = Environment.CurrentDirectory;

           // var execPath = System.IO.Path.GetDirectory(Application.ExecutablePath);
            if (File.Exists(envCurrDir + @"\MLBYearByYearLeagueBattingStats.json"))
            {
                using (StreamReader r = new StreamReader(envCurrDir + @"\MLBYearByYearLeagueBattingStats.json"))
                {
                    string json = r.ReadToEnd();
                    _mLBYearByYearBattingStatsCache = JsonConvert.DeserializeObject<IEnumerable<MLBYearByYearBattingStatsDTO>>(json).ToList();
                }
            }
        }

        private static void LoadPitchingJson()
        {
            string envCurrDir = Environment.CurrentDirectory;
            if (File.Exists(envCurrDir+ @"\MLBYearByYearLeaguePitchingStats.json"))
            {
                using (StreamReader r = new StreamReader(envCurrDir + @"\MLBYearByYearLeaguePitchingStats.json"))
                {
                    string json = r.ReadToEnd();
                    _mLBYearByYearPitchingStatsCache = JsonConvert.DeserializeObject<IEnumerable<MLBYearByYearPitchingStatsDTO>>(json).ToList();
                }
            }
        }
    }
}
