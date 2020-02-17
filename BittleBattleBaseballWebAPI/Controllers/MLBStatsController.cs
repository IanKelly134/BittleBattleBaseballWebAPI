using BittleBattleBaseball.ApplicationService;
using BittleBattleBaseball.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BittleBattleBaseballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLBStatsController : ControllerBase
    {
        [HttpGet("{season}", Name = "GetLeagueBattingStatsByYear")]
        public MLBYearByYearBattingStatsViewModel GetLeagueBattingStatsByYear(int season)
        {
            MLBStatsApplicationService appService = new MLBStatsApplicationService();
            var stats = appService.GetLeagueBattingStatsByYear(season);
            return stats;
        }

        [HttpGet("{season}", Name = "GetLeaguePitchingStatsByYear")]
        public MLBYearByYearPitchingStatsViewModel GetLeaguePitchingStatsByYear(int season)
        {
            MLBStatsApplicationService appService = new MLBStatsApplicationService();
            var stats = appService.GetLeaguePitchingStatsByYear(season);
            return stats;
        }
    }
}
