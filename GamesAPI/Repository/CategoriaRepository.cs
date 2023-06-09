using GamesAPI.Context;
using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) 
        {
        
        }

        public IEnumerable<Categoria> GetCategoriasJogos()
        {
            return Get().Include(x => x.Jogos);
        }
    }
}
