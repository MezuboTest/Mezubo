using api.roulette.Entities;
using System.Threading.Tasks;

namespace api.roulette.Services.IServices
{
    interface IBetService
    {
        Task CreateBet(string rouletteId, string user, BetRequets betRequets);
        Task ValidateBet(BetRequets betRequets);
    }
}
