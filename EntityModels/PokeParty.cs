using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class PokeParty
    {
        public int TrainerId { get; set; }
        public int MyPokeId { get; set; }

        public virtual MyPokemon MyPoke { get; set; } = null!;
        public virtual PokeTrainer Trainer { get; set; } = null!;
    }
}
