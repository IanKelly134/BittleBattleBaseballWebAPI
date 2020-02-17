namespace BittleBattleBaseball.Models.DTOs
{
    public class MLBYearByYearPitchingStatsDTO
    {
        public int Year { get; set; }
        public int Tms { get; set; }
        //    public int __invalid_name__#P { get; set; }
        public double PAge { get; set; }
        //   public double __invalid_name__R/G { get; set; }
        public double ERA { get; set; }
        public int G { get; set; }
        public double GF { get; set; }
        public double CG { get; set; }
        public double SHO { get; set; }
        public double tSho { get; set; }
        public double SV { get; set; }
        public double IP { get; set; }
        public double H { get; set; }
        public double R { get; set; }
        public double ER { get; set; }
        public double HR { get; set; }
        public double BB { get; set; }
        public object IBB { get; set; }
        public double SO { get; set; }
        public object HBP { get; set; }
        public double BK { get; set; }
        public double WP { get; set; }
        public double BF { get; set; }
        // public string __invalid_name__ERA+ { get; set; }
        public double WHIP { get; set; }
        public object BAbip { get; set; }
        public double H9 { get; set; }
        public double HR9 { get; set; }
        public double BB9 { get; set; }
        public double SO9 { get; set; }
        //public double __invalid_name__SO/W { get; set; }
        public double E { get; set; }
    }
}
