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
    public class ParticipantesController : ControllerBase
    {
        private readonly ActTallerContext _context;

        public ParticipantesController(ActTallerContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            if (participante == null)
            {
                return NotFound();
            }

            return participante;
        }

        // POST: api/Participantes
        [HttpPost]
        public async Task<ActionResult<Participante>> CreateParticipante(Participante participante)
        {
            if (ModelState.IsValid)
            {
                _context.Participantes.Add(participante);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
            }

            return BadRequest();
        }

        // PUT: api/Participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, Participante participante)
        {
            if (id != participante.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(participante).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
