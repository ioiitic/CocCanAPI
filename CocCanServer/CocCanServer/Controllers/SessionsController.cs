using AutoMapper;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _SessionService;

        public SessionsController(ISessionService SessionService)
        {
            _SessionService = SessionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Session>))]
        public async Task<IActionResult> GetAll(string search, int from, int to, string filter, string orderBy, bool ascending)
        {
            var Sessions = await _SessionService.GetAllSessionsWithStatusAsync(search, from, to, filter, orderBy, ascending);
            return Ok(Sessions);
        }
    }
}
