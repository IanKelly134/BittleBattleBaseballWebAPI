using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BittleBattleBaseball.Models.DTOs
{
    public class GetPlayerSeasonHittingStatsQueryResultsMulti
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public IEnumerable<Stat> row { get; set; }
    }

    public class SportHittingTm
    {
        public string copyRight { get; set; }
        public GetPlayerSeasonHittingStatsQueryResultsMulti queryResults { get; set; }
    }

    public partial class GetPlayerSeasonHittingMultiTeamStatsDTO
    {
        public SportHittingTm sport_hitting_tm { get; set; }
    }

    public partial class GetPlayerSeasonHittingMultiTeamStatsDTO
    {
        public static GetPlayerSeasonHittingMultiTeamStatsDTO FromJson(string json) => JsonConvert.DeserializeObject<GetPlayerSeasonHittingMultiTeamStatsDTO>(json, Converter.Settings);
    }

    public static class GetPlayerSeasonHittingStatsDTOSerialize
    {
        public static string ToJson(this GetPlayerSeasonHittingMultiTeamStatsDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
