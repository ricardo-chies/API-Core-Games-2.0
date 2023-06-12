using GamesAPI.Models;
using GamesAPI.Pagination;

namespace GamesAPI.Repository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        IEnumerable<Jogo> GetJogoPorPreco();
        PagedList<Jogo> GetJogos(JogosParameters jogosParameters);
    }
}
