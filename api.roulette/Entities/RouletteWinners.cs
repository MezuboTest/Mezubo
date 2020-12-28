using System.Collections.Generic;

namespace api.roulette.Entities
{
    public class RouletteWinners
    {
        public string Id { get; set; }

        public int NumberWin { get; set; }

        public List<Bet> Winners { get; set; }
        public List<Bet> Losers { get; set; }
    }
}
