using GamesAPI.Context;
using GamesAPI.Models;

namespace GamesAPI.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(AppDbContext context) : base(context) 
        { 

        }
        public IEnumerable<Jogo> GetJogoPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
