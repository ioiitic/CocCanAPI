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
        public async Task<IActionResult> GetAll(string filter)
        {
            var Sessions = await _SessionService.GetAllSessionsWithStatusAsync(filter);
            return Ok(Sessions);
        }
    }
}
