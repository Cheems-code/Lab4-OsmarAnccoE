using Lab4_OsmarAnccoE.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4_OsmarAnccoE.Repository.Implementation;

public class OrdeneRepository : Repository<Ordene, int>, IOrdene
{
    public OrdeneRepository(AdbContextn context) : base(context)
    {
        
    }
}