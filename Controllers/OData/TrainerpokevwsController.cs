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
    public class TrainerpokevwsController : ODataController
    {
        /*
         Inject your context to controller to get access to EF
         */
        private readonly pokedbContext _context;
        public TrainerpokevwsController(pokedbContext context)
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
            return Ok(_context.Trainerpokevws);
        }

        /* 
         Get a single record given its key
         localhost:5000/Trainerpokevws('key')
        */
        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var Trainerpokevw = await _context.Trainerpokevws.FindAsync(key);
            if (Trainerpokevw == null)
            {
                return NotFound();
            }

            return Ok(Trainerpokevw);
        }

        /*
         It is like a SQL update where, given a key, you can update specific attributes without the need to specify the whole object
         Limitations: Cannot update values in an array or remove attributes.
         */
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Trainerpokevw> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Trainerpokevw = await _context.Trainerpokevws.FindAsync(key);
            if (Trainerpokevw == null)
            {
                return NotFound();
            }

            model.Patch(Trainerpokevw);

            await _context.SaveChangesAsync();

            return Updated(Trainerpokevw);
        }

        /*
         Insert new record given all attributes match exactly with the object that is expected
         */
        public async Task<IActionResult> Post([FromBody] Trainerpokevw model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainerpokevws.Add(model);
            await _context.SaveChangesAsync();

            return Created(model);
        }

        /*
         Update an Trainerpokevws given the key and specifying all attributes (This is a must in order to update something via Put Verb)
         */
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Trainerpokevw update)
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
            var Trainerpokevw = await _context.Trainerpokevws.FindAsync(key);
            if (Trainerpokevw == null)
            {
                return NotFound();
            }

            _context.Trainerpokevws.Remove(Trainerpokevw);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}