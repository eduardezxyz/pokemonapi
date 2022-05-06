using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class MoveVsType
    {
        public int MoveId { get; set; }
        public int TypeId { get; set; }
        public int? Ratio { get; set; }

        public virtual PokeMove Move { get; set; } = null!;
        public virtual PokeType Type { get; set; } = null!;
    }
}
