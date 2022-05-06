using Demoapi.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Demoapi.Controllers.OData
{
    /*
 NOTE: Controller name has to match file name and remember it extends ODataController class!!!
 */
    public class PokeTypesController : ODataController
    {
        /*
         Inject your context to controller to get access to EF
         */
        private readonly pokedbContext _context;
        public PokeTypesController(pokedbContext context)
        {
            _context = context;
        }

        /* 
         Get many or all records depends on method 
         Example calls can be found in: https://docs.microsoft.com/en-us/odata/concepts/queryoptions-overview
         */
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.PokeTypes);
        }

        /* 
         Get a single record given its key
         localhost:5000/PokeTypes('key')
        */
        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var PokeType = await _context.PokeTypes.FindAsync(key);
            if (PokeType == null)
            {
                return NotFound();
            }

            return Ok(PokeType);
        }

        /*
         It is like a SQL update where, given a key, you can update specific attributes without the need to specify the whole object
         Limitations: Cannot update values in an array or remove attributes.
         */
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<PokeType> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PokeType = await _context.PokeTypes.FindAsync(key);
            if (PokeType == null)
            {
                return NotFound();
            }

            model.Patch(PokeType);

            await _context.SaveChangesAsync();

            return Updated(PokeType);
        }

        /*
         Insert new record given all attributes match exactly with the object that is expected
         */
        public async Task<IActionResult> Post([FromBody] PokeType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PokeTypes.Add(model);
            await _context.SaveChangesAsync();

            return Created(model);
        }

        /*
         Update an PokeTypes given the key and specifying all attributes (This is a must in order to update something via Put Verb)
         */
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] PokeType update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != update.Id)
            {
                return BadRequest();
            }

            _context.Entry(update).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Updated(update);
        }

        /*
         Delete a record given a key
         In the company we don't tend to use hard deletes a lot, we use soft deletes.
          - Hard delete: Deleting the record permanently
          - Soft delete: (A bit better and perhaps used in the company often) Setting an isDeleted record to true or updating the column 
         */
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var PokeType = await _context.PokeTypes.FindAsync(key);
            if (PokeType == null)
            {
                return NotFound();
            }

            _context.PokeTypes.Remove(PokeType);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}