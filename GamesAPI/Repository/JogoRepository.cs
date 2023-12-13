using GamesAPI.Context;
using GamesAPI.Models;
using GamesAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(AppDbContext context, ILogger<JogoRepository> logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Jogo>> GetJogoPorPreco()
        {
            return await Get().OrderBy(c => c.Preco).ToListAsync();
        }

        public async Task<PagedList<Jogo>> GetJogos(JogosParameters jogosParameters)
        {
            return await PagedList<Jogo>.ToPagedList(Get().OrderBy(on => on.JogoId),
                jogosParameters.PageNumber, jogosParameters.PageSize);
        }
    }
}
