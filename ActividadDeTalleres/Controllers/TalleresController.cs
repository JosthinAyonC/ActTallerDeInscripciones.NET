using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActividadDeTalleres.Models;

namespace ActividadDeTalleres.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TalleresController : ControllerBase
    {
        private readonly ActTallerContext _context;

        public TalleresController(ActTallerContext context)
        {
            _context = context;
        }

        // GET: api/Talleres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tallere>>> GetTalleres()
        {
            return await _context.Talleres.ToListAsync();
        }

        // GET: api/Talleres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tallere>> GetTaller(int id)
        {
            var tallere = await _context.Talleres.FindAsync(id);

            if (tallere == null)
            {
                return NotFound();
            }

            return tallere;
        }

        // POST: api/Talleres
        [HttpPost]
        public async Task<ActionResult<Tallere>> CreateTaller(Tallere tallere)
        {
            if (ModelState.IsValid)
            {
                _context.Talleres.Add(tallere);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTaller), new { id = tallere.Id }, tallere);
            }

            return BadRequest();
        }

        // PUT: api/Talleres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaller(int id, Tallere tallere)
        {
            if (id != tallere.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(tallere).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Talleres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaller(int id)
        {
            var tallere = await _context.Talleres.FindAsync(id);
            if (tallere == null)
            {
                return NotFound();
            }

            _context.Talleres.Remove(tallere);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
