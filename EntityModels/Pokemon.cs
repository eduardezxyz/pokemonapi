using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            MyPokemons = new HashSet<MyPokemon>();
            Moves = new HashSet<PokeMove>();
        }

        public string? PokeName { get; set; }
        public decimal? HeightMeters { get; set; }
        public decimal? WeightKg { get; set; }
        public int PokeType1Id { get; set; }
        public int? PokeType2Id { get; set; }
        public int PokedexEntry { get; set; }

        public virtual PokeType PokeType1 { get; set; } = null!;
        public virtual PokeType? PokeType2 { get; set; }
        public virtual ICollection<MyPokemon> MyPokemons { get; set; }

        public virtual ICollection<PokeMove> Moves { get; set; }
    }
}
