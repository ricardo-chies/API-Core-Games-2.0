using GamesAPI.Context;
using GamesAPI.Models;
using GamesAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context) 
        {
        
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasJogos()
        {
            return await Get().Include(x => x.Jogos).ToListAsync();
        }

        public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters)
        {
            return await PagedList<Jogo>.ToPagedList(Get().OrderBy(on => on.CategoriaId),
                categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }
    }
}
