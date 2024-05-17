using System.Collections.Generic;
using System.Threading.Tasks;
using BittleBattleBaseball.ApplicationService;
using BittleBattleBaseball.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BittleBattleBaseballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        /// <summary>
        /// GET: api/Team/mlb/1987
        /// </summary>
        /// <param name="league"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        [HttpGet("{league}/{season}", Name = "GetTeamsBySeason")]
        public async Task<List<TeamSearchResultViewModel>> GetTeamsBySeason(string league, int season)
        {
            TeamApplicationService appService = new TeamApplicationService();
            var result = await appService.GetTeamsBySeason(league, season);
            return result;
        }

        /// <summary>
        /// GET: api/Team/mlb/1987/138/false
        /// </summary>
        /// <param name="league"></param>
        /// <param name="season"></param>
        /// <param name="teamId"></param>
        /// <param name="isDesignatedHitterEnabled"></param>
        /// <returns></returns>
        [HttpGet("{league}/{season}/{teamId}/{isDesignatedHitterEnabled}", Name = "GetRosterBySeason")]
        public async Task<RosterSearchResultViewModel> GetRosterBySeason(string league, int season, int teamId, bool isDesignatedHitterEnabled)
        {
            TeamApplicationService appService = new TeamApplicationService();
            var roster = await appService.GetRosterBySeason(league, season, teamId, isDesignatedHitterEnabled);
            return roster;
        }
    }
}
