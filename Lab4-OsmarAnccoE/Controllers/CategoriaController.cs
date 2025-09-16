using Lab4_OsmarAnccoE.Dtos.Categoria;
using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_OsmarAnccoE.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var categorias = await _unitOfWork.Categoria.GetAllAsync();
            return Ok(categorias);
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _unitOfWork.Categoria.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaCreateDto categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = new Categoria
            {
                Nombre = categoriaDto.Nombre
            };

            await _unitOfWork.Categoria.AddAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { id = categoria.Categoriaid }, categoria);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] CategoriaUpdateDto categoriaDto)
        {
            var categoriaExistente = await _unitOfWork.Categoria.GetByIdAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapeamos los datos del DTO a la entidad existente
            categoriaExistente.Nombre = categoriaDto.Nombre;

            _unitOfWork.Categoria.Update(categoriaExistente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _unitOfWork.Categoria.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _unitOfWork.Categoria.Delete(categoria);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }