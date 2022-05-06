using Microsoft.AspNetCore.Mvc;
using Demoapi.Models;
using Demoapi.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly pokedbContext _pokedbContext;

        public PokemonController(pokedbContext context)
        {
            _pokedbContext = context;
        }

        [HttpGet("GetPokeAndType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPokeAndType(string? name, string? type)
        {
            var pokeref = _pokedbContext.Pokemons.
            Include(x => x.PokeType1).
            Include(x => x.PokeType2).
            Where(x => x.PokeType1.PokeTypeName == type || x.PokeType2.PokeTypeName == type).
            Select(x => (PokeList)x).ToList(); 
            return Ok(pokeref);
        }

        [HttpGet("GetAllorSingle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllOrSingle([FromQuery] string? name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return Ok(_pokedbContext.Pokemons);
            }
            else
            {
                return Ok(_pokedbContext.Pokedetails.Where(result => result.PokeName.Contains(name)));
            }
        }

        [HttpGet("GetPokeMovesTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPokeMovesTypes([FromQuery] string? name, [FromQuery] string? type, [FromQuery] string? move)
        {
        
            if (!String.IsNullOrEmpty(name)){
                var pokeref = _pokedbContext.Pokedetails.Where(x => x.PokeName.Contains(name));
                return Ok(pokeref);
            }
            else if (!String.IsNullOrEmpty(type)) {
                var pokeref = _pokedbContext.Pokedetails.Where(x => x.Type1.Contains(type) || x.Type2.Contains(type));
                return Ok(pokeref);
            }
            else if (!String.IsNullOrEmpty(move)){
                var pokeref = _pokedbContext.Pokedetails.Where(x => x.MoveList.Contains(move));
                return Ok(pokeref);
            }
            else {
                return Ok(_pokedbContext.Pokedetails);
            } 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertPokemon([FromBody] NewPokemonRequest _newPokemonRequest)
        {

            Pokemon myNewPokemon = new Pokemon
            {
                PokedexEntry = _newPokemonRequest.pokedex_entry,
                PokeName = _newPokemonRequest.poke_name,
                HeightMeters = _newPokemonRequest.poke_height,
                WeightKg = _newPokemonRequest.poke_weight,
                PokeType1Id = _newPokemonRequest.poke_type1,
                PokeType2Id = _newPokemonRequest.poke_type2
            };

            _pokedbContext.Pokemons.Add(myNewPokemon);
            _pokedbContext.SaveChanges();
            return Ok(myNewPokemon);
        }

        [HttpPost("{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePokemon([FromBody] NewPokemonRequest _newPokemonRequest, string Name)
        {
            Pokemon myNewPokemon = new Pokemon
            {
                PokedexEntry = _newPokemonRequest.pokedex_entry,
                PokeName = Name,
                HeightMeters = _newPokemonRequest.poke_height,
                WeightKg = _newPokemonRequest.poke_weight,
                PokeType1Id = _newPokemonRequest.poke_type1,
                PokeType2Id = _newPokemonRequest.poke_type2
            };
            // var newPoke = _pokedbContext.Pokemons.Where(result => result.PokeName.Contains(Name)).First();

            _pokedbContext.Pokemons.Update(myNewPokemon);
            _pokedbContext.SaveChanges();
            return Ok(myNewPokemon);
        }

        [HttpDelete("{pokedex_num}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePokemon(int pokedex_num)
        {
            var newPoke = _pokedbContext.Pokemons.Where(result => result.PokedexEntry == pokedex_num).Include(result => result.MyPokemons).FirstOrDefault();
            _pokedbContext.Pokemons.Attach(newPoke ?? new Pokemon());
            _pokedbContext.Pokemons.Remove(newPoke);
            _pokedbContext.SaveChanges();
            return Ok(newPoke);
        }

    }
}