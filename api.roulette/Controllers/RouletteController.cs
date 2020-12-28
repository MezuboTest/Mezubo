using api.roulette.Entities;
using api.roulette.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api.roulette.Controllers
{

    [Route("roulette")]
    [ApiController]
    public class RouletteController : Controller
    {
        private readonly RouletteService rouletteService;
        private readonly BetService betService;
        public RouletteController()
        {
            rouletteService = new RouletteService();
            betService = new BetService();
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {

            return Ok("Prueba Mezubo");
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await rouletteService.CreateRoulette();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoulette(string id)
        {
            var result = await rouletteService.GetRoulette(id);

            return Ok(result);
        }

        [HttpPost("opening/{id}")]
        public async Task<IActionResult> OpenigRoulette(string id)
        {
            var result = await rouletteService.OpeningRoulette(id);

            return Ok(result);
        }

        [HttpPost("closing/{id}")]
        public async Task<IActionResult> ClosingRoulette(string id)
        {
            var result = await rouletteService.ClosingRoulette(id);

            return Ok(result);
        }

        [HttpPost("bet/{id}")]
        public async Task<IActionResult> Bet(string id, BetRequets betRequets)
        {

            string user = Request.Headers["user"];
            if (string.IsNullOrEmpty(user))
                return BadRequest();
            try
            {

                await betService.CreateBet(id, user, betRequets);
            }
            catch (ApplicationException ex)
            {

                return BadRequest(ex.Message);
            }


            return Ok();
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListRoulette()
        {
            var result = await rouletteService.ListRoulette();
            return Ok(result);
        }


    }
}
