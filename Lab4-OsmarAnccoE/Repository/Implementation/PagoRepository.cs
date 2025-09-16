using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository.Implementation;

public class PagoRepository:Repository<Pago, int>, IPago
{
    public PagoRepository(AdbContextn contextn) : base(contextn)
    {
        
    }
}