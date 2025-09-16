using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository.Implementation;

public class ProductoRepository : Repository<Producto, int>, IProducto
{
    public ProductoRepository(AdbContextn contextn) : base(contextn)
    {
        
    }
}