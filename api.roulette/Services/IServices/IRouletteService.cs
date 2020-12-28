using api.roulette.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.roulette.Services.IServices
{
    interface IRouletteService
    {
        Task<Roulette> CreateRoulette();
        Task<Roulette> GetRoulette(string id);

        Task<Roulette> OpeningRoulette(string id);
        Task UpdateRoulette(Roulette roulette);
        Task<RouletteWinners> ClosingRoulette(string id);

        Task<List<Roulette>> ListRoulette();

    }
}
