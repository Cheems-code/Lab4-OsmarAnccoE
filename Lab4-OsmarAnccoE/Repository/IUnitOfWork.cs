namespace Lab4_OsmarAnccoE.Repository;

public interface IUnitOfWork: IDisposable
{
    IClienteRepository Clientes { get; }
    ICategoria Categoria{ get; }
    IDetallesOrden DetallesOrden { get; }
    IOrdene Ordene { get; }
    IPago Pago { get; }
    IProducto Producto { get; }
    
    Task<int> SaveChangesAsync();
}