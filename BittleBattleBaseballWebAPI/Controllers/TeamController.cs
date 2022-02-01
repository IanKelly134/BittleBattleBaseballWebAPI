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
        //// GET: api/Team/mlb/1987
        [HttpGet("{league}/{season}", Name = "GetTeamsBySeason")]
        public async Task<List<TeamSearchResultViewModel>> GetTeamsBySeason(string league, int season)
        {
            TeamApplicationService appService = new TeamApplicationService();
            return await appService.GetTeamsBySeason(league, season);
        }

        //// GET: api/TeamSeasonRoster/1987/138
        [HttpGet("{league}/{season}/{teamId}", Name = "GetRosterBySeason")]
        public async Task<RosterSearchResultViewModel> GetRosterBySeason(string league, int season, int teamId)
        {
            TeamApplicationService appService = new TeamApplicationService();
            var roster = await appService.GetRosterBySeason(league, season, teamId);
            return roster;
        }

        // POST: api/Team
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
