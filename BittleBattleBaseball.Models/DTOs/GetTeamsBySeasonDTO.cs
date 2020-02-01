using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BittleBattleBaseball.Models.DTOs
{
    public class TeamSearchResult
    {
        public string phone_number { get; set; }
        public string venue_name { get; set; }
        public string franchise_code { get; set; }
        public string all_star_sw { get; set; }
        public string sport_code { get; set; }
        public string address_city { get; set; }
        public string city { get; set; }
        public string name_display_full { get; set; }
        public string spring_league_abbrev { get; set; }
        public string time_zone_alt { get; set; }
        public string sport_id { get; set; }
        public string venue_id { get; set; }
        public string mlb_org_id { get; set; }
        public string time_zone_generic { get; set; }
        public string mlb_org { get; set; }
        public string last_year_of_play { get; set; }
        public string league_full { get; set; }
        public string home_opener_time { get; set; }
        public string address_province { get; set; }
        public string league_id { get; set; }
        public string name_abbrev { get; set; }
        public string bis_team_code { get; set; }
        public string league { get; set; }
        public string spring_league { get; set; }
        public string base_url { get; set; }
        public string address_zip { get; set; }
        public string sport_code_display { get; set; }
        public string mlb_org_short { get; set; }
        public string time_zone { get; set; }
        public string address_line1 { get; set; }
        public string mlb_org_brief { get; set; }
        public string address_line2 { get; set; }
        public string season { get; set; }
        public string address_line3 { get; set; }
        public string division_abbrev { get; set; }
        public string name_display_short { get; set; }
        public string team_id { get; set; }
        public string active_sw { get; set; }
        public string address_intl { get; set; }
        public string state { get; set; }
        public string address_country { get; set; }
        public string mlb_org_abbrev { get; set; }
        public string division { get; set; }
        public string team_code { get; set; }
        public string name { get; set; }
        public string website_url { get; set; }
        public string sport_code_name { get; set; }
        public string first_year_of_play { get; set; }
        public string league_abbrev { get; set; }
        public string name_display_long { get; set; }
        public string store_url { get; set; }
        public string time_zone_text { get; set; }
        public string name_short { get; set; }
        public object home_opener { get; set; }
        public string address_state { get; set; }
        public string division_full { get; set; }
        public string time_zone_num { get; set; }
        public string spring_league_full { get; set; }
        public string address { get; set; }
        public string name_display_brief { get; set; }
        public string file_code { get; set; }
        public string division_id { get; set; }
        public string spring_league_id { get; set; }
        public string venue_short { get; set; }
    }

    public class QueryResults
    {
        public DateTime created { get; set; }
        public string totalSize { get; set; }
        public List<TeamSearchResult> row { get; set; }
    }

    public class TeamAllSeason
    {
        public string copyRight { get; set; }
        public QueryResults queryResults { get; set; }
    }

    public partial class GetTeamsBySeasonDTO
    {
        public TeamAllSeason team_all_season { get; set; }
    }

    public partial class GetTeamsBySeasonDTO
    {
        public static GetTeamsBySeasonDTO FromJson(string json) => JsonConvert.DeserializeObject<GetTeamsBySeasonDTO>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetTeamsBySeasonDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
