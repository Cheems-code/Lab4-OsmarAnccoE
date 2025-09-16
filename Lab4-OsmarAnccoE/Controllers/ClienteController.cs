using Lab4_OsmarAnccoE.Dtos.Cliente;
using Lab4_OsmarAnccoE.Models;
using Lab4_OsmarAnccoE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_OsmarAnccoE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapeo: Convertimos el DTO a la entidad del dominio
            var cliente = new Cliente
            {
                Nombre = clienteDto.Nombre,
                Correo = clienteDto.Correo
            };

            await _unitOfWork.Clientes.AddAsync(cliente);
            await _unitOfWork.SaveChangesAsync();

            // Devolvemos la entidad completa, no el DTO
            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.Clienteid }, cliente);
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _unitOfWork.Clientes.GetAllAsync();
            return Ok(clientes);
        }
        
        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound(); 
            }

            return Ok(cliente);
        }
        
        // PUT: api/Cliente/1
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Primero, obtenemos la entidad existente de la base de datos
            var clienteExistente = await _unitOfWork.Clientes.GetByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound("El cliente que intentas actualizar no existe.");
            }

            //Mapeamos los valores del DTO a la entidad existente
            clienteExistente.Nombre = clienteDto.Nombre;
            clienteExistente.Correo = clienteDto.Correo;

            //Llamamos a Update en la entidad ya modificada
            _unitOfWork.Clientes.Update(clienteExistente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        
        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            _unitOfWork.Clientes.Delete(cliente);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}