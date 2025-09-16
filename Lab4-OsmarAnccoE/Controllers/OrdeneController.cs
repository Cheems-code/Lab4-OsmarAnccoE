using Lab4_OsmarAnccoE.Dtos.Ordene;
using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_OsmarAnccoE.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Orden
        [HttpGet]
        public async Task<IActionResult> ObtenerOrdenes()
        {
            var ordenes = await _unitOfWork.Ordene.GetAllAsync();
            return Ok(ordenes);
        }

        // GET: api/Orden/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrdenPorId(int id)
        {
            var orden = await _unitOfWork.Ordene.GetByIdAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        // POST: api/Orden
        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] OrdeneCreateDto ordenDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Validar que el cliente exista
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(ordenDto.ClienteID);
            if (cliente == null) return NotFound("El cliente especificado no existe.");
            
            decimal totalOrden = 0;
            var detallesOrden = new List<Detallesorden>();

            //Procesar cada detalle de la orden
            foreach (var detalleDto in ordenDto.Detalles)
            {
                var producto = await _unitOfWork.Producto.GetByIdAsync(detalleDto.ProductoID);
                if (producto == null) return NotFound($"El producto con ID {detalleDto.ProductoID} no existe.");
                if (producto.Stock < detalleDto.Cantidad) return BadRequest($"Stock insuficiente para el producto '{producto.Nombre}'.");

                //Descontar stock y calcular total
                producto.Stock -= detalleDto.Cantidad;
                _unitOfWork.Producto.Update(producto);
                
                totalOrden += producto.Precio * detalleDto.Cantidad;

                detallesOrden.Add(new Detallesorden
                {
                    Productoid = detalleDto.ProductoID,
                    Cantidad = detalleDto.Cantidad,
                    Precio = producto.Precio 
                });
            }

            //Crear la entidad Orden
            var nuevaOrden = new Ordene()
            {
                Clienteid = ordenDto.ClienteID,
                Total = totalOrden,
                Fechaorden = System.DateTime.Now,
                Detallesordens = detallesOrden
            };

            await _unitOfWork.Ordene.AddAsync(nuevaOrden);
            
            //Guardar todos los cambios en una sola transacción
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerOrdenPorId), new { id = nuevaOrden.Ordenid }, nuevaOrden);
        }

        // PUT: api/Orden/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarOrden(int id, [FromBody] OrdeneUpdateDto ordenDto)
        {
            var ordenExistente = await _unitOfWork.Ordene.GetByIdAsync(id);
            if (ordenExistente == null) return NotFound();

            var cliente = await _unitOfWork.Clientes.GetByIdAsync(ordenDto.ClienteID);
            if (cliente == null) return NotFound("El nuevo cliente especificado no existe.");

            ordenExistente.Clienteid = ordenDto.ClienteID;
            _unitOfWork.Ordene.Update(ordenExistente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Orden/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            var orden = await _unitOfWork.Ordene.GetByIdAsync(id);
            if (orden == null) return NotFound();

            _unitOfWork.Ordene.Delete(orden);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }