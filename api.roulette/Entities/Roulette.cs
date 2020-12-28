using System.Collections.Generic;

namespace api.roulette.Entities
{
    public class Roulette
    {
        public string Id { get; set; }

        public bool Status { get; set; }

        public List<Bet> Bets { get; set; }

        public int NumberWin { get; set; }


    }
}
