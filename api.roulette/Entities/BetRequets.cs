using System;
using System.ComponentModel.DataAnnotations;

namespace api.roulette.Entities
{
    [Serializable]
    public class BetRequets
    {

        [Range(0, 38)]
        public int BetNumber { get; set; }

        [Range(minimum: 1, maximum: 10000, ErrorMessage = "Rango de apuesta debe estar entre 1 y 10000.")]
        public int BetAmount { get; set; }

        [RegularExpression(@"(?:rojo|negro)", ErrorMessage = "Color no valido.")]
        public string BetColor { get; set; }
    }
}
