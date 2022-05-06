using Demoapi.EntityModels;

namespace Demoapi.Models
{
    public class PokemonModel
    {

    }

    public class PokeList {
        public string poke_type1_name { get; set;}

        public string poke_type2_name { get; set;}

        public string poke_name {get; set;}

        public static explicit operator PokeList(Pokemon entityPokemon) {
            return new PokeList{
                poke_name = entityPokemon.PokeName,
                poke_type1_name = entityPokemon.PokeType1.PokeTypeName,
                poke_type2_name = entityPokemon.PokeType2?.PokeTypeName
            };
        }
    }

    public class NewPokemonRequest{
        public int pokedex_entry { get; set;}

        public string poke_name { get; set;}

        public decimal poke_height { get; set;}

        public decimal poke_weight { get; set;}

        public int poke_type1 { get; set;}

        public int? poke_type2 { get; set;} = null;
    }

    public class ReturnPokemon {
        public string poke_name { get; set;} 

        public int poke_height { get; set;}

    }

}