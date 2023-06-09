using GamesAPI.Models;

namespace GamesAPI.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasJogos();
    }
}
