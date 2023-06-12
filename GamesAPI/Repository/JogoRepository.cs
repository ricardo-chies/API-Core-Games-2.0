using GamesAPI.Context;
using GamesAPI.Models;
using GamesAPI.Pagination;

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

        public PagedList<Jogo> GetJogos(JogosParameters jogosParameters)
        {
            return PagedList<Jogo>.ToPagedList(Get().OrderBy(on => on.JogoId),
                jogosParameters.PageNumber, jogosParameters.PageSize);
        }
    }
}
