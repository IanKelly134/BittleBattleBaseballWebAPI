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
        //// GET: api/Team/5
        [HttpGet("{season}", Name = "GetTeamsBySeason")]
        public async Task<List<TeamSearchResultViewModel>> GetTeamsBySeason(int season)
        {
            TeamApplicationService appService = new TeamApplicationService();
            return await appService.GetTeamsBySeason(season);
        }

        //// GET: api/Team/5
        [HttpGet("{season}/{teamId}", Name = "GetRosterBySeason")]
        public async Task<RosterSearchResultViewModel> GetRosterBySeason(int season, int teamId)
        {
            TeamApplicationService appService = new TeamApplicationService();
            var roster = await appService.GetRosterBySeason(season, teamId);
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
