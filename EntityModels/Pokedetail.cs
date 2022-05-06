using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class Pokedetail
    {
        public string? PokeName { get; set; }
        public string? MoveList { get; set; }
        public string? Type1 { get; set; }
        public string? Type2 { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? HeightMeters { get; set; }
        public int? PokedexEntry { get; set; }
    }
}
