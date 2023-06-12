using GamesAPI.Models;
using GamesAPI.Pagination;

namespace GamesAPI.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasJogos();
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
    }
}
