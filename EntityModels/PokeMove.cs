using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class PokeMove
    {
        public PokeMove()
        {
            MoveVsTypes = new HashSet<MoveVsType>();
            PokedexEntries = new HashSet<Pokemon>();
        }

        public int Id { get; set; }
        public string? MoveName { get; set; }

        public virtual ICollection<MoveVsType> MoveVsTypes { get; set; }

        public virtual ICollection<Pokemon> PokedexEntries { get; set; }
    }
}
