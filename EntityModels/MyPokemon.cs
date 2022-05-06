using System;
using System.Collections.Generic;

namespace Demoapi.EntityModels
{
    public partial class MyPokemon
    {
        public int Id { get; set; }
        public int PokedexEntry { get; set; }
        public string? Nickname { get; set; }
        public int Hp { get; set; }
        public int PokeLvl { get; set; }

        public virtual Pokemon PokedexEntryNavigation { get; set; } = null!;
    }
}
