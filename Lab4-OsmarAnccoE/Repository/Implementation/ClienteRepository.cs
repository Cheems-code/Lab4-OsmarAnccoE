using Microsoft.EntityFrameworkCore; 
using Lab4_OsmarAnccoE.Models; 


namespace Lab4_OsmarAnccoE.Repository.Implementation
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AdbContextn _context;

        public ClienteRepository(AdbContextn context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Set<Cliente>().FindAsync(id); // Usa FindAsync
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Set<Cliente>().ToListAsync(); // Usa ToListAsync
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Set<Cliente>().AddAsync(cliente); // Usa AddAsync
        }

        public void Update(Cliente cliente)
        {
            _context.Set<Cliente>().Update(cliente); 
        }

        public void Delete(Cliente cliente)
        {
            _context.Set<Cliente>().Remove(cliente); 
        }
    } 
}