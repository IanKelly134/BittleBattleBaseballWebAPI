namespace BittleBattleBaseball.Models.DTOs
{
    public class MLBYearByYearBattingStatsDTO
    {
        public int Year { get; set; }
        public int Tms { get; set; }
        //   public int __invalid_name__#Bat { get; set; }
        public double BatAge { get; set; }
        //    public double __invalid_name__RperG { get; set; }
        public int G { get; set; }
        public double PA { get; set; }
        public double AB { get; set; }
        public double R { get; set; }
        public double H { get; set; }
        public double __invalid_name__2B { get; set; }
        public double __invalid_name__3B { get; set; }
        public double HR { get; set; }
        public object RBI { get; set; }
        public object SB { get; set; }
        public object CS { get; set; }
        public double BB { get; set; }
        public double SO { get; set; }
        public double BA { get; set; }
        public double OBP { get; set; }
        public double SLG { get; set; }
        public double OPS { get; set; }
        public double TB { get; set; }
        public object GDP { get; set; }
        public object HBP { get; set; }
        public object SH { get; set; }
        public object SF { get; set; }
        public object IBB { get; set; }
    }
}
