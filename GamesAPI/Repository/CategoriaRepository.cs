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

        public IEnumerable<Categoria> GetCategoriasJogos()
        {
            return Get().Include(x => x.Jogos);
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            return PagedList<Jogo>.ToPagedList(Get().OrderBy(on => on.CategoriaId),
                categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }
    }
}
