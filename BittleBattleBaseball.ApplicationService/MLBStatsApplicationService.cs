using BittleBattleBaseball.Models.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BittleBattleBaseball.ApplicationService
{
    public class MLBStatsApplicationService
    {
        private static List<MLBYearByYearBattingStatsDTO> _mLBYearByYearBattingStatsCache;
        private static List<MLBYearByYearPitchingStatsDTO> _mLBYearByYearPitchingStatsCache;

        public bool HasChanges { get; set; }

        public static List<MLBYearByYearBattingStatsDTO> MLBYearByYearBattingStatsCache
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

        public static List<MLBYearByYearPitchingStatsDTO> MLBYearByYearPitchingStatsCache
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

        public static void LoadBattingJson()
        {
            if (File.Exists(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\MLBYearByYearLeagueBattingStats.json"))
            {
                using (StreamReader r = new StreamReader(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\MLBYearByYearLeagueBattingStats.json"))
                {
                    string json = r.ReadToEnd();
                    _mLBYearByYearBattingStatsCache = JsonConvert.DeserializeObject<IEnumerable<MLBYearByYearBattingStatsDTO>>(json).ToList();
                }
            }
        }

        public static void LoadPitchingJson()
        {
            if (File.Exists(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\MLBYearByYearLeaguePitchingStats.json"))
            {
                using (StreamReader r = new StreamReader(@"C:\DEV\BittleBattleBaseballWebAPI\BittleBattleBaseballWebAPI\MLBYearByYearLeaguePitchingStats.json"))
                {
                    string json = r.ReadToEnd();
                    _mLBYearByYearPitchingStatsCache = JsonConvert.DeserializeObject<IEnumerable<MLBYearByYearPitchingStatsDTO>>(json).ToList();
                }
            }
        }
    }
}
