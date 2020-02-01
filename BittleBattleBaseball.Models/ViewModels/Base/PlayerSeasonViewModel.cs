using System;
using System.Collections.Generic;
using System.Text;

namespace BittleBattleBaseball.Models.ViewModels.Base
{
    public class PlayerSeasonViewModel
    {
        /// <summary>
        /// Basic Player Info
        /// </summary>
        public PlayerViewModel Player { get; set; }

        /// <summary>
        /// Year Of Stats
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        /// Type of game, regular season = 'R'
        /// </summary>
        public string GameType { get; set; }

        /// <summary>
        /// League Type, usually 'mlb'
        /// </summary>
        public string LeagueType { get; set; }

        /// <summary>
        /// Fielding Percentage
        /// </summary>
        public decimal FldPct { get; set; }

      
    }
}
