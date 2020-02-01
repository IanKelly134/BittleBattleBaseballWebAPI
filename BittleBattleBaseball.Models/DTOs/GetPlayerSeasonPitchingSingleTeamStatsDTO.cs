using Newtonsoft.Json;
using System;

namespace BittleBattleBaseball.Models.DTOs
{
    public class GetPlayerSeasonPitchingStatsQueryResultsSingle
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public PitchingStat row { get; set; }
    }

    public class SportPitchingTmSingle
    {
        public string copyRight { get; set; }
        public GetPlayerSeasonPitchingStatsQueryResultsSingle queryResults { get; set; }
    }

    public partial class GetPlayerSeasonPitchingSingleTeamStatsDTO
    {
        public SportPitchingTmSingle sport_pitching_tm { get; set; }
    }

    public partial class GetPlayerSeasonPitchingSingleTeamStatsDTO
    {
        public static GetPlayerSeasonPitchingSingleTeamStatsDTO FromJson(string json) => JsonConvert.DeserializeObject<GetPlayerSeasonPitchingSingleTeamStatsDTO>(json, Converter.Settings);
    }

    public static class GetPlayerSeasonPitchingSingleTeamStatsDTOSerialize
    {
        public static string ToJson(this GetPlayerSeasonPitchingSingleTeamStatsDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
