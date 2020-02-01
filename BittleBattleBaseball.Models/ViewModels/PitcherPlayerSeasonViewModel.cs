using BittleBattleBaseball.Models.ViewModels.Base;

namespace BittleBattleBaseball.Models.ViewModels
{
    public class PitcherPlayerSeasonViewModel : PlayerSeasonViewModel
    {
        /// <summary>
        /// Pitcher Walks Plus Hits Per Inning Average
        /// </summary>
        public decimal WHIP { get; set; }

        /// <summary>
        /// Pitcher Earned Run Average
        /// </summary>
        public decimal ERA { get; set; }

        /// <summary>
        /// Season Wins
        /// </summary>
        public int Wins { get; set; }

        //Season Losses
        public int Losses { get; set; }
    }
}
