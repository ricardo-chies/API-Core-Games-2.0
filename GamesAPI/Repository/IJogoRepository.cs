using GamesAPI.Models;
using GamesAPI.Pagination;

namespace GamesAPI.Repository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<IEnumerable<Jogo>> GetJogoPorPreco();
        Task<PagedList<Jogo>> GetJogos(JogosParameters jogosParameters);
    }
}
