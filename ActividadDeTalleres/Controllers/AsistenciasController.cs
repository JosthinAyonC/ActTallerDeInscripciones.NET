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
    public class AsistenciasController : ControllerBase
    {
        private readonly ActTallerContext _context;

        public AsistenciasController(ActTallerContext context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias()
        {
            return await _context.Asistencias.ToListAsync();
        }

        // GET: api/Asistencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencia>> GetAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return asistencia;
        }

        // POST: api/Asistencias
        [HttpPost]
        public async Task<ActionResult<Asistencia>> CreateAsistencia(Asistencia asistencia)
        {
            asistencia.Participante = await _context.Participantes.FindAsync(asistencia.ParticipanteId);
            asistencia.Taller = await _context.Talleres.FindAsync(asistencia.TallerId);
            
            if (ModelState.IsValid)
            {
                _context.Asistencias.Add(asistencia);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAsistencia), new { id = asistencia.Id }, asistencia);
            }
            

            return BadRequest();
        }

        // PUT: api/Asistencias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsistencia(int id, Asistencia asistencia)
        {
            if (id != asistencia.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(asistencia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Asistencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
