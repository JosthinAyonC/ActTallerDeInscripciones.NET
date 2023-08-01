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
    public class InscripcionesController : ControllerBase
    {
        private readonly ActTallerContext _context;

        public InscripcionesController(ActTallerContext context)
        {
            _context = context;
        }

        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcione>>> GetInscripciones()
        {
            return await _context.Inscripciones.ToListAsync();
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcione>> GetInscripcion(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);

            if (inscripcione == null)
            {
                return NotFound();
            }

            return inscripcione;
        }

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<Inscripcione>> CreateInscripcion(Inscripcione inscripcione)
        {
            if (ModelState.IsValid)
            {
                var taller = await _context.Talleres.FindAsync(inscripcione.TallerId);
                if (inscripcione == null)
                {
                    return NotFound();
                }else{
                    if (taller.CupoMaximo <= 0)
                    {
                        return BadRequest();
                    }else{
                        taller.CupoMaximo -= 1;
                    }
                }
                var participante = await _context.Participantes.FindAsync(inscripcione.ParticipanteId);
                if (participante == null)
                {
                    return NotFound();
                }else{
                    inscripcione.Participante = participante;
                }
                
                _context.Inscripciones.Add(inscripcione);

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcione.Id }, inscripcione);
            }

            return BadRequest();
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInscripcion(int id, Inscripcione inscripcione)
        {
            if (id != inscripcione.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(inscripcione).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            _context.Inscripciones.Remove(inscripcione);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
