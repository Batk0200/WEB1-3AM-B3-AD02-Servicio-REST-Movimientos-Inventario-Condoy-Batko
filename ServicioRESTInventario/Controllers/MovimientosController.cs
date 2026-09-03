using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioRESTInventario.Data;
using ServicioRESTInventario.Models;

namespace ServicioRESTInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        // Obtener lista de movimientos activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        {
            var movimientos = await _context.MovimientosInventario
                .Where(m => m.Estado == true)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToListAsync();

            return Ok(movimientos);
        }

        // GET: api/Movimientos/5
        // Obtener un movimiento por Id
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario
                .FirstOrDefaultAsync(m => m.IdMovimiento == id && m.Estado == true);

            if (movimiento == null)
            {
                return NotFound(new { mensaje = "Movimiento no encontrado o está inactivo." });
            }

            return Ok(movimiento);
        }

        // POST: api/Movimientos
        // Guardar un nuevo movimiento
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> PostMovimiento(MovimientoInventario movimiento)
        {
            // Validaciones básicas
            if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
            {
                return BadRequest(new { mensaje = "TipoMovimiento solo puede ser 'Entrada' o 'Salida'." });
            }

            if (movimiento.Cantidad <= 0)
            {
                return BadRequest(new { mensaje = "La cantidad debe ser mayor a 0." });
            }

            // Asignar valores por defecto
            movimiento.Estado = true;
            movimiento.FechaMovimiento = DateTime.Now;

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.IdMovimiento }, movimiento);
        }

        // PUT: api/Movimientos/5
        // Actualizar un movimiento
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, MovimientoInventario movimiento)
        {
            if (id != movimiento.IdMovimiento)
            {
                return BadRequest(new { mensaje = "El Id de la URL no coincide con el Id del objeto." });
            }

            var movimientoExistente = await _context.MovimientosInventario
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimientoExistente == null)
            {
                return NotFound(new { mensaje = "Movimiento no encontrado." });
            }

            // Validaciones
            if (movimiento.TipoMovimiento != "Entrada" && movimiento.TipoMovimiento != "Salida")
            {
                return BadRequest(new { mensaje = "TipoMovimiento solo puede ser 'Entrada' o 'Salida'." });
            }

            if (movimiento.Cantidad <= 0)
            {
                return BadRequest(new { mensaje = "La cantidad debe ser mayor a 0." });
            }

            // Actualizar campos
            movimientoExistente.IdProducto = movimiento.IdProducto;
            movimientoExistente.TipoMovimiento = movimiento.TipoMovimiento;
            movimientoExistente.Cantidad = movimiento.Cantidad;
            movimientoExistente.Observacion = movimiento.Observacion;
            // No modificamos FechaMovimiento ni Estado aquí

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Movimiento actualizado correctamente." });
        }

        // DELETE: api/Movimientos/5
        // Eliminar (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimiento == null)
            {
                return NotFound(new { mensaje = "Movimiento no encontrado." });
            }

            if (!movimiento.Estado)
            {
                return BadRequest(new { mensaje = "El movimiento ya se encuentra desactivado." });
            }

            // Soft Delete
            movimiento.Estado = false;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Movimiento desactivado correctamente." });
        }
    }
}