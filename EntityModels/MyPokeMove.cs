using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class MyPokeMove
    {
        public int MyPokeId { get; set; }
        public int MoveId { get; set; }

        public virtual PokeMove Move { get; set; } = null!;
        public virtual MyPokemon MyPoke { get; set; } = null!;
    }
}
