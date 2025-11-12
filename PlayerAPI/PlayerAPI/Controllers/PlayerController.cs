using Microsoft.AspNetCore.Mvc;
using PlayerAPI.Models;

namespace PlayerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private static List<Player> players = new List<Player>()
        {
            new Player { Id = 1, Vida = 100, QuantidadeItens = 2, PosicaoX = 0, PosicaoY = 0, PosicaoZ = 0 },
            new Player { Id = 2, Vida = 80, QuantidadeItens = 5, PosicaoX = 10, PosicaoY = 2, PosicaoZ = 5 }
        };

        [HttpGet("GetPlayers")]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return Ok(players);
        }

        [HttpGet("GetPlayer/{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player == null)
                return NotFound("Player não encontrado.");
            return Ok(player);
        }

        [HttpPost("AddPlayer")]
        public ActionResult<Player> AddPlayer([FromBody] Player newPlayer)
        {
            if (players.Any(p => p.Id == newPlayer.Id))
                return BadRequest("Já existe um player com esse ID.");

            players.Add(newPlayer);
            return Ok(newPlayer);
        }

        [HttpDelete("DeletePlayer/{id}")]
        public ActionResult DeletePlayer(int id)
        {
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player == null)
                return NotFound("Player não encontrado.");

            players.Remove(player);
            return Ok($"Player com ID {id} removido.");
        }

        [HttpPut("UpdatePlayer/{id}")]
        public ActionResult UpdatePlayer(int id, [FromBody] Player updatedPlayer)
        {
            var player = players.FirstOrDefault(p => p.Id == id);
            if (player == null)
                return NotFound("Player não encontrado.");

            player.Vida = updatedPlayer.Vida;
            player.QuantidadeItens = updatedPlayer.QuantidadeItens;
            player.PosicaoX = updatedPlayer.PosicaoX;
            player.PosicaoY = updatedPlayer.PosicaoY;
            player.PosicaoZ = updatedPlayer.PosicaoZ;

            return Ok(player);
        }
    }
}
