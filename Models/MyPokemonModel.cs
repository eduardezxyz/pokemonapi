using System.ComponentModel.DataAnnotations;
namespace Demoapi.Models
{
    public class CreateMyPokemon{
        public int Pokedex_Entry { get; set;}

        public string Nickname { get; set;}

        public int HealthPoints { get; set;}

        public int Poke_Level { get; set;}
    }

    public class UpdateMyPokemon{
        public string Nickname { get; set;}

        public int HealthPoints { get; set;}

        public int Poke_Level { get; set;}
    }

    public class MyPokemonPost{
        public int Pokedex_Entry { get; set;}

        public string Nickname { get; set;}
    }

}
