using GamesAPI.Models;

namespace GamesAPI.Repository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        IEnumerable<Jogo> GetJogoPorPreco();
    }
}
