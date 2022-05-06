using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class PokeTrainer
    {
        public int Id { get; set; }
        public string TrainerName { get; set; } = null!;
    }
}
