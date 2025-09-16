using Lab4_OsmarAnccoE.Dtos.Pago;
using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_OsmarAnccoE.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PagoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<IActionResult> ObtenerPagos()
        {
            var pagos = await _unitOfWork.Pago.GetAllAsync();
            return Ok(pagos);
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPagoPorId(int id)
        {
            var pago = await _unitOfWork.Pago.GetByIdAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            return Ok(pago);
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<IActionResult> RegistrarPago([FromBody] PagoCreateDto pagoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Validar que la orden exista
            var orden = await _unitOfWork.Ordene.GetByIdAsync(pagoDto.OrdenID);
            if (orden == null) return NotFound($"La orden con ID {pagoDto.OrdenID} no existe.");

            //Validar que el pago no exceda el total de la orden
            if (pagoDto.Monto > orden.Total)
            {
                return BadRequest($"El monto del pago (${pagoDto.Monto}) excede el total de la orden (${orden.Total}).");
            }
            //Crear la entidad Pago
            var nuevoPago = new Pago
            {
                Ordenid = pagoDto.OrdenID,
                Monto = pagoDto.Monto,
                Metodopago = pagoDto.MetodoPago,
                Fechapago = System.DateTime.Now
            };

            await _unitOfWork.Pago.AddAsync(nuevoPago);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPagoPorId), new { id = nuevoPago.Pagoid }, nuevoPago);
        }
        
        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPago(int id, [FromBody] PagoUpdateDto pagoDto)
        {
            var pagoExistente = await _unitOfWork.Pago.GetByIdAsync(id);
            if (pagoExistente == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            pagoExistente.Metodopago = pagoDto.MetodoPago;
            _unitOfWork.Pago.Update(pagoExistente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPago(int id)
        {
            var pago = await _unitOfWork.Pago.GetByIdAsync(id);
            if (pago == null) return NotFound();

            _unitOfWork.Pago.Delete(pago);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }