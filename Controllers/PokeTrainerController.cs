using Microsoft.AspNetCore.Mvc;
using Demoapi.Models;
using Demoapi.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokeTrainerController : ControllerBase
    {
        private readonly pokedbContext _pokedbContext;

        public PokeTrainerController (pokedbContext context) {
            _pokedbContext = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllOrSingle([FromQuery]string? name)
        {
            if (String.IsNullOrEmpty(name)) {
                return Ok(_pokedbContext.PokeTrainers);
            }
            else {
                return Ok(_pokedbContext.PokeTrainers.Where(result => result.TrainerName.Contains(name)));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult InsertNewTrainer([FromBody] InsertNewTrainer _newTrainer){ 
            
            PokeTrainer newTrainer = new PokeTrainer {
                TrainerName = _newTrainer.TrainerName
            };

            _pokedbContext.PokeTrainers.Add(newTrainer);
            _pokedbContext.SaveChanges();
            return Ok(newTrainer);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateNewTrainer([FromBody] UpdateTrainer _newTrainer, int id){ 
            
            var newTrainer = _pokedbContext.PokeTrainers.First(result => result.Id == id);

            if (!String.IsNullOrEmpty(_newTrainer.TrainerName)) {
                newTrainer.TrainerName = _newTrainer.TrainerName;
            }
            else {
                return BadRequest("Trainer name can't be null or empty");
            }

            _pokedbContext.PokeTrainers.Update(newTrainer);
            _pokedbContext.SaveChanges();
            return Ok(newTrainer);
        }

        [HttpDelete("DeleteMyPokemon/{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteMyPoke(int ID) {
            _pokedbContext.Database.ExecuteSqlRaw("call poke_party_delete({0})", ID);
            return Ok();
        }
    }
    
}