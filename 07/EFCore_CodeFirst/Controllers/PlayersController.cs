using System.Threading.Tasks;
using EFCore_CodeFirst.DTO;
using EFCore_CodeFirst.DTO.Players;
using EFCore_CodeFirst.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPlayerAsync([FromBody] CreatePlayerRequest playerRequest)
        {
            if (!ModelState.IsValid) { return BadRequest();}
            await _playerService.CreatePlayerAsync(playerRequest);
            return Ok("Record has been added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayersAsync([FromQuery] UrlQueryParameters urlQueryParameters)
        {
            var player = await _playerService.GetPlayersAsync(urlQueryParameters);
            if (player == null) { return NotFound();}
            return Ok(player);
        }

        [HttpGet("{id:long}/detail")]
        public async Task<IActionResult> GetPlayerDetailAsync(int id)
        {
            var player = await _playerService.GetPlayerDetailAsync(id);
            // null validation check
            return Ok(player);
        }
    }
}