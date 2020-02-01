using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BittleBattleBaseball.Models.DTOs
{
    public class Row
    {
        public string name_first_last { get; set; }
        public string weight { get; set; }
        public string primary_position { get; set; }
        public DateTime birth_date { get; set; }
        public string throws { get; set; }
        public string stat_years { get; set; }
        public string height_inches { get; set; }
        public string name_sort { get; set; }
        public string status_short { get; set; }
        public string jersey_number { get; set; }
        public string player_first_last_html { get; set; }
        public string bats { get; set; }
        public string primary_position_cd { get; set; }
        public string position_desig { get; set; }
        public string forty_man_sw { get; set; }
        public string player_html { get; set; }
        public string height_feet { get; set; }
        public string player_id { get; set; }
        public string name_last_first { get; set; }
        public string current_sw { get; set; }
        public string roster_years { get; set; }
        public string team_id { get; set; }
        public string active_sw { get; set; }
    }

    public class GetRosterBySeasonDTOQueryResults
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public List<Row> row { get; set; }
    }

    public class RosterTeamAlltime
    {
        public string copyRight { get; set; }
        public GetRosterBySeasonDTOQueryResults queryResults { get; set; }
    }

    public partial class GetRosterBySeasonDTO
    {
        public RosterTeamAlltime roster_team_alltime { get; set; }
    }


    public partial class GetRosterBySeasonDTO
    {
        public static GetRosterBySeasonDTO FromJson(string json) => JsonConvert.DeserializeObject<GetRosterBySeasonDTO>(json, Converter.Settings);
    }

    public static class GetRosterBySeasonDTOSerialize
    {
        public static string ToJson(this GetRosterBySeasonDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

   

}
