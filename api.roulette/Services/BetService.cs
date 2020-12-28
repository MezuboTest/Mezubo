using api.roulette.Entities;
using api.roulette.Repository;
using api.roulette.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.roulette.Services
{
    public class BetService : IBetService
    {
        private readonly RedisConnection _redisServices;
        private readonly RouletteService _rouletteService;
        public BetService()
        {
            _redisServices = new RedisConnection();
            _rouletteService = new RouletteService();
        }
        public async Task CreateBet(string rouletteId, string user, BetRequets betRequets)
        {
            await ValidateBet(betRequets);

            Roulette roulette = await _rouletteService.GetRoulette(rouletteId);
            if (!roulette.Status) throw new ApplicationException("La ruleta se encuentra cerrada.");
            if (roulette.Bets == null) roulette.Bets = new List<Bet>();

            roulette.Bets.Add(new Bet
            {
                User = user,
                BetAmount = betRequets.BetAmount,
                BetNumber = betRequets.BetNumber,
                BetColor = betRequets.BetColor

            });

            await _rouletteService.UpdateRoulette(roulette);

        }

        public async Task ValidateBet(BetRequets betRequets)
        {
            if (betRequets.BetNumber == 0 && string.IsNullOrEmpty(betRequets.BetColor)) throw new ApplicationException("Debe de realizar apuesta a un número o color");
            if (betRequets.BetNumber > 0 && !string.IsNullOrEmpty(betRequets.BetColor)) throw new ApplicationException("Solo puede apostar a un número o a un color, pero no ambos al mismo tiempo.");
            if (betRequets.BetAmount < 0) throw new ApplicationException("Debe de apostar un monto mayor a 1 y menor de 10000");

        }
    }
}