using Lab4_OsmarAnccoE.Dtos.Producto;
using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_OsmarAnccoE.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _unitOfWork.Producto.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id)
        {
            var producto = await _unitOfWork.Producto.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoCreateDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                Categoriaid = productoDto.CategoriaID
            };

            await _unitOfWork.Producto.AddAsync(producto);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Productoid }, producto);
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoUpdateDto productoDto)
        {
            var productoExistente = await _unitOfWork.Producto.GetByIdAsync(id);
            if (productoExistente == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapeamos los datos del DTO a la entidad existente
            productoExistente.Nombre = productoDto.Nombre;
            productoExistente.Descripcion = productoDto.Descripcion;
            productoExistente.Precio = productoDto.Precio;
            productoExistente.Stock = productoDto.Stock;
            productoExistente.Categoriaid = productoDto.CategoriaID;

            _unitOfWork.Producto.Update(productoExistente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            Producto? producto = await _unitOfWork.Producto.GetByIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _unitOfWork.Producto.Delete(producto);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }