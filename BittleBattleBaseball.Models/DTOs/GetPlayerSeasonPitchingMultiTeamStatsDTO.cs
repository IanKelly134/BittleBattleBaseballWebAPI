using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BittleBattleBaseball.Models.DTOs
{
    public class PitchingStat
    {
        public string gidp { get; set; }
        public string h9 { get; set; }
        public string sac { get; set; }
        public string np { get; set; }
        public string tr { get; set; }
        public string gf { get; set; }
        public string sport_code { get; set; }
        public string bqs { get; set; }
        public string hgnd { get; set; }
        public string sho { get; set; }
        public string bq { get; set; }
        public string gidp_opp { get; set; }
        public string bk { get; set; }
        public string kbb { get; set; }
        public string sport_id { get; set; }
        public string hr9 { get; set; }
        public string sv { get; set; }
        public string slg { get; set; }
        public string bb { get; set; }
        public string whip { get; set; }
        public string avg { get; set; }
        public string ops { get; set; }
        public string team_full { get; set; }
        public string db { get; set; }
        public string league_full { get; set; }
        public string team_abbrev { get; set; }
        public string hfly { get; set; }
        public string so { get; set; }
        public string tbf { get; set; }
        public string bb9 { get; set; }
        public string league_id { get; set; }
        public string wp { get; set; }
        public string sf { get; set; }
        public string team_seq { get; set; }
        public string hpop { get; set; }
        public string league { get; set; }
        public string hb { get; set; }
        public string cs { get; set; }
        public string pgs { get; set; }
        public string season { get; set; }
        public string sb { get; set; }
        public string go_ao { get; set; }
        public string ppa { get; set; }
        public string cg { get; set; }
        public string player_id { get; set; }
        public string gs { get; set; }
        public string ibb { get; set; }
        public string team_id { get; set; }
        public string pk { get; set; }
        public string go { get; set; }
        public string hr { get; set; }
        public string irs { get; set; }
        public string wpct { get; set; }
        public string era { get; set; }
        public string babip { get; set; }
        public DateTime end_date { get; set; }
        public string rs9 { get; set; }
        public string qs { get; set; }
        public string league_short { get; set; }
        public string g { get; set; }
        public string ir { get; set; }
        public string hld { get; set; }
        public string k9 { get; set; }
        public string sport { get; set; }
        public string team_short { get; set; }
        public string l { get; set; }
        public string svo { get; set; }
        public string h { get; set; }
        public string ip { get; set; }
        public string obp { get; set; }
        public string w { get; set; }
        public string hldr { get; set; }
        public string ao { get; set; }
        public string s { get; set; }
        public string r { get; set; }
        public string spct { get; set; }
        public string pip { get; set; }
        public string ab { get; set; }
        public string er { get; set; }
    }

    public class GetPlayerSeasonPitchingStatsQueryResultsMultiple
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public IEnumerable<PitchingStat> row { get; set; }
    }

    public class SportPitchingTm
    {
        public string copyRight { get; set; }
        public GetPlayerSeasonPitchingStatsQueryResultsMultiple queryResults { get; set; }
    }

    public partial class GetPlayerSeasonPitchingMultiTeamStatsDTO
    {
        public SportPitchingTm sport_pitching_tm { get; set; }
    }

    public partial class GetPlayerSeasonPitchingMultiTeamStatsDTO
    {
        public static GetPlayerSeasonPitchingMultiTeamStatsDTO FromJson(string json) => JsonConvert.DeserializeObject<GetPlayerSeasonPitchingMultiTeamStatsDTO>(json, Converter.Settings);
    }

    public static class GetPlayerSeasonPitchingMultiTeamStatsDTOSerialize
    {
        public static string ToJson(this GetPlayerSeasonPitchingMultiTeamStatsDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
