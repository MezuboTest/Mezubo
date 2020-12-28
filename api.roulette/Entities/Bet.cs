namespace api.roulette.Entities
{
    public class Bet
    {
        public int BetNumber { get; set; }

        public int BetAmount { get; set; }

        public string BetColor { get; set; }

        public string User { get; set; }

        public double AmountGain { get; set; }
    }
}
