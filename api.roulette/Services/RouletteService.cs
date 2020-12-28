using api.roulette.Entities;

using api.roulette.Repository;
using api.roulette.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.roulette.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly RedisConnection _redisServices;
        private readonly Random _random = new Random();
        private const double _gaintWithNumber = 5;
        private const double _gaintWithColor = 1.8;
        public RouletteService()
        {
            _redisServices = new RedisConnection();
        }



        public async Task<Roulette> CreateRoulette()
        {

            Roulette roulette = new Roulette
            {
                Id = Guid.NewGuid().ToString(),
                Status = false,
                Bets = null
            };
            _redisServices.Set(roulette.Id, JsonConvert.SerializeObject(roulette));

            return roulette;
        }



        public async Task<Roulette> GetRoulette(string id)
        {
            Roulette roulette = JsonConvert.DeserializeObject<Roulette>(_redisServices.Get(id));

            return roulette;
        }

        public async Task<Roulette> OpeningRoulette(string id)
        {
            Roulette roulette = JsonConvert.DeserializeObject<Roulette>(_redisServices.Get(id));
            if (!roulette.Status)
                roulette.Status = true;
            _redisServices.Set(roulette.Id, JsonConvert.SerializeObject(roulette));
            return roulette;

        }

        public async Task UpdateRoulette(Roulette roulette)
        {
            _redisServices.Set(roulette.Id, JsonConvert.SerializeObject(roulette));


        }

        public async Task<RouletteWinners> ClosingRoulette(string id)
        {
            Roulette roulette = JsonConvert.DeserializeObject<Roulette>(_redisServices.Get(id));
            if (roulette.Status)
            {
                roulette.NumberWin = _random.Next(0, 38);
                roulette.Status = false;
                UpdateRoulette(roulette);
            }
            else
            {
                if (roulette.NumberWin == 0) throw new ApplicationException("La ruleta aun no ha sido jugada.");

            }


            return CalculateWinners(roulette);
        }

        private RouletteWinners CalculateWinners(Roulette roulette)
        {

            RouletteWinners rouletteWinners = new RouletteWinners
            {
                Id = roulette.Id,
                NumberWin = roulette.NumberWin
            };

            string colorWin = (roulette.NumberWin % 2) == 0 ? "rojo" : "negro";


            rouletteWinners.Winners = roulette.Bets.Where(p => p.BetNumber == roulette.NumberWin || p.BetColor == colorWin).ToList();

            foreach (Bet bet in rouletteWinners.Winners)
            {
                if (bet.BetNumber > 0)
                {
                    bet.AmountGain = bet.BetAmount * _gaintWithNumber;
                }
                else
                {
                    bet.AmountGain = bet.BetAmount * _gaintWithColor;
                }
            }
            rouletteWinners.Losers = roulette.Bets.Where(p => p.BetNumber != roulette.NumberWin || (p.BetColor != colorWin && p.BetNumber != roulette.NumberWin)).ToList();

            return rouletteWinners;

        }

        public async Task<List<Roulette>> ListRoulette()
        {
            List<string> lstRouletSerialize = _redisServices.GetAllKeys();
            List<Roulette> lstRolette = new List<Roulette>();
            foreach (string bet in lstRouletSerialize)
            {
                lstRolette.Add(JsonConvert.DeserializeObject<Roulette>(bet));
            }
            return lstRolette;
        }
    }
}
