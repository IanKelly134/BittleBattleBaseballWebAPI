using System.Collections.Generic;
using BittleBattleBaseball.ApplicationService;
using BittleBattleBaseball.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BittleBattleBaseballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // GET: api/Team
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/Team/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// GET: api/Team/5
        [HttpGet("{season}", Name = "GetTeamsBySeason")]
        public List<TeamSearchResultViewModel> GetTeamsBySeason(int season)
        {
            TeamApplicationService appService = new TeamApplicationService();
            return appService.GetTeamsBySeason(season);
        }

        //// GET: api/Team/5
        [HttpGet("{season}/{teamId}", Name = "GetRosterBySeason")]
        public RosterSearchResultViewModel GetRosterBySeason(int season, int teamId)
        {
            TeamApplicationService appService = new TeamApplicationService();
            var roster = appService.GetRosterBySeason(season, teamId);
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
