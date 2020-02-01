using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BittleBattleBaseball.Models.DTOs
{
    public class ImageSearchResult
    {
        public string title { get; set; }
        public string kwic { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string iurl { get; set; }
        public string domain { get; set; }
        public string author { get; set; }
        public bool news { get; set; }
        public string votes { get; set; }
        public object date { get; set; }
        public List<object> related { get; set; }
    }

    public partial class GetPlayerImagesDTO
    {
        public List<ImageSearchResult> results { get; set; }
        public string query { get; set; }
        //public List<object> suggestions { get; set; }
        public int count { get; set; }
        //public int start { get; set; }
        //public int length { get; set; }
        //public string time { get; set; }
    }

    public partial class GetPlayerImagesDTO
    {
        public static GetPlayerImagesDTO FromJson(string json) => JsonConvert.DeserializeObject<GetPlayerImagesDTO>(json, Converter.Settings);
    }

    public static class GetPlayerImagesDTOSerialize
    {
        public static string ToJson(this GetPlayerImagesDTO self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
