using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository.Implementation;

public class DetallesOrdenRepository : Repository<Detallesorden, int>, IDetallesOrden
{
    public DetallesOrdenRepository(AdbContextn contextn) : base(contextn)
    {
        
    }
}