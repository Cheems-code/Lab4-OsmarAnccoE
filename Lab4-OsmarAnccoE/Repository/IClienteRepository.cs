using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(int id);

        Task<IEnumerable<Cliente>> GetAllAsync();

        Task AddAsync(Cliente cliente);

        void Update(Cliente cliente);
        
        void Delete(Cliente cliente);
    }
}