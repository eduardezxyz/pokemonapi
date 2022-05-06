using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class PokeType
    {
        public PokeType()
        {
            MoveVsTypes = new HashSet<MoveVsType>();
            PokemonPokeType1s = new HashSet<Pokemon>();
            PokemonPokeType2s = new HashSet<Pokemon>();
        }

        public int Id { get; set; }
        public string? PokeTypeName { get; set; }

        public virtual ICollection<MoveVsType> MoveVsTypes { get; set; }
        public virtual ICollection<Pokemon> PokemonPokeType1s { get; set; }
        public virtual ICollection<Pokemon> PokemonPokeType2s { get; set; }
    }
}
