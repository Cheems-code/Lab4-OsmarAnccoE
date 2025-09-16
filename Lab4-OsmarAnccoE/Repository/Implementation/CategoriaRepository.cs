using Lab4_OsmarAnccoE.Models;

namespace Lab4_OsmarAnccoE.Repository.Implementation;

public class CategoriaRepository : Repository<Categoria, int>, ICategoria
{
    public CategoriaRepository(AdbContextn contextn) : base(contextn)
    {
        
    }
}