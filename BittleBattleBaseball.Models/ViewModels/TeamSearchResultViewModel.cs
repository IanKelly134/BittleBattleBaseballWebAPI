namespace BittleBattleBaseball.Models.ViewModels
{
    public class TeamSearchResultViewModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Season { get; set; }
        public string League { get; set; }
        public string Ballpark { get; set; }
        public string City { get; set; }
        public string NameAbbrev { get; set; }
        public string Name { get; set; }

        public string FullTeamName { get; set; }

        public string LogoUrl { get; set; }

    }
}
