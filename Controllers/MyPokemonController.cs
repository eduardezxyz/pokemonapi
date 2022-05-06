using Microsoft.AspNetCore.Mvc;
using Demoapi.EntityModels;
using Demoapi.Models;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyPokemonController: ControllerBase
    {
        private readonly pokedbContext _pokedbContext;

        public MyPokemonController (pokedbContext context){
            _pokedbContext = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllOrSingle([FromQuery]string? name){
            
            if (String.IsNullOrEmpty(name)) {
                return Ok(_pokedbContext.MyPokemons);
            }
            else {
                return Ok(_pokedbContext.MyPokemons.Where(result => result.Nickname.Contains(name)));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertMyPokemon([FromBody] CreateMyPokemon _myPokemon){ 
            
            MyPokemon createMyPoke = new MyPokemon {
                PokedexEntry = _myPokemon.Pokedex_Entry,
                Nickname = _myPokemon.Nickname,
                Hp = _myPokemon.HealthPoints,
                PokeLvl = _myPokemon.Poke_Level
            };

            _pokedbContext.MyPokemons.Add(createMyPoke);
            _pokedbContext.SaveChanges();
            return Ok(createMyPoke);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMyPokemon([FromBody] UpdateMyPokemon _myPokemon, int id){ 

            var newPoke = _pokedbContext.MyPokemons.First(result => result.Id == id);

            if (!String.IsNullOrEmpty(_myPokemon.Nickname)) {
                newPoke.Nickname = _myPokemon.Nickname;
            }

            if (_myPokemon.HealthPoints != 0) {
                newPoke.Hp = _myPokemon.HealthPoints;
            }

            if (_myPokemon.Poke_Level != 0) {
                newPoke.PokeLvl = _myPokemon.Poke_Level;
            }

            _pokedbContext.MyPokemons.Update(newPoke);
            _pokedbContext.SaveChanges();
            return Ok(newPoke);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteMyPokemon(int id){ 

            if (_pokedbContext.PokeParties.Any(x => x.MyPokeId == id)) {
                var pokePartyRef = _pokedbContext.PokeParties.Where(result => result.MyPokeId == id).Include(result => result.MyPoke).First();
                _pokedbContext.PokeParties.Attach(pokePartyRef);
                _pokedbContext.PokeParties.Remove(pokePartyRef);
                _pokedbContext.MyPokemons.RemoveRange(pokePartyRef.MyPoke);
                // return BadRequest("Pokemon is currently part of your party");
            }
            else {
                var myPokeRef = _pokedbContext.MyPokemons.Where(result => result.Id == id).First();
                _pokedbContext.MyPokemons.Attach(myPokeRef ?? new MyPokemon());
                _pokedbContext.MyPokemons.Remove(myPokeRef);
            }

            _pokedbContext.SaveChanges();
            return Ok();
        }
    }
}