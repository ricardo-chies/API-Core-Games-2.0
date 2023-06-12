using GamesAPI.Models;
using GamesAPI.Pagination;

namespace GamesAPI.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasJogos();
        Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriasParameters);
    }
}
