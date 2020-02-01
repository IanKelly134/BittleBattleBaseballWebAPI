using BittleBattleBaseball.Models.ViewModels.Base;

namespace BittleBattleBaseball.Models.ViewModels
{
    public class HitterPlayerSeasonViewModel : PlayerSeasonViewModel
    {      

        /// <summary>
        /// On-Base Percentage
        /// </summary>
        public decimal OBP { get; set; }

        /// <summary>
        /// Slugging Percentage
        /// </summary>
        public decimal SLG { get; set; }

        /// <summary>
        /// Batting Average
        /// </summary>
        public decimal AVG { get; set; }

        /// <summary>
        /// Plate Appearances
        /// </summary>
        public int PA { get; set; }

        /// <summary>
        /// At-Bats
        /// </summary>
        public int AB { get; set; }

        /// <summary>
        /// Home Runs
        /// </summary>
        public int HR { get; set; }

        /// <summary>
        /// RBIs
        /// </summary>
        public int RBI { get; set; }

        /// <summary>
        /// Stolen Bases
        /// </summary>
        public int SB { get; set; }

        /// <summary>
        /// Caught Stealing
        /// </summary>
        public int CS { get; set; }

        /// <summary>
        /// Walks
        /// </summary>
        public int BB { get; set; }

        /// <summary>
        /// Grounded Into Double Plays
        /// </summary>
        public int GIDP { get; set; }

        /// <summary>
        /// Hits
        /// </summary>
        public int H { get; set; }

        /// <summary>
        /// Runs
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Strike-outs
        /// </summary>
        public int SO { get; set; }

        /// <summary>
        /// Extra Base Hits
        /// </summary>
        public int XBH { get; set; }
    }
}
