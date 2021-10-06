using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BittleBattleBaseball.Models.DTOs
{

    public class GetPlayerProjectedSeasonHittingSingleTeamStats
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public Stat row { get; set; }
    }

    public class ProjPecotaBatting
    {
        public string copyRight { get; set; }
        public GetPlayerProjectedSeasonHittingSingleTeamStats queryResults { get; set; }
    }

    public partial class GetPlayerProjectedSeasonHittingSingleTeamStatsDTO
    {
        public ProjPecotaBatting proj_pecota_batting { get; set; }
    }

    public partial class GetPlayerProjectedSeasonHittingSingleTeamStatsDTO
    {
        public static GetPlayerProjectedSeasonHittingSingleTeamStatsDTO FromJson(string json) => JsonConvert.DeserializeObject<GetPlayerProjectedSeasonHittingSingleTeamStatsDTO>(json, Converter.Settings);
    }

    public static class GetPlayerProjectedSeasonHittingSingleTeamStatsDTOSerialize
    {
        public static string ToJson(this GetPlayerProjectedSeasonHittingSingleTeamStatsDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
