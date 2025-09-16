using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdbContextn _context; 

        public IClienteRepository Clientes { get; private set; }
        public ICategoria Categoria { get; private set; } 
        public IDetallesOrden DetallesOrden { get; private set; } 
        public IOrdene Ordene { get; private set; } 
        public IPago Pago { get; private set; } 
        public IProducto Producto { get; private set; } 

        public UnitOfWork(AdbContextn context)
        {
            _context = context;

            Clientes = new ClienteRepository(_context);
            Categoria = new CategoriaRepository(_context);
            DetallesOrden = new DetallesOrdenRepository(_context);
            Ordene = new OrdeneRepository(_context);
            Pago = new PagoRepository(_context);
            Producto = new ProductoRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}