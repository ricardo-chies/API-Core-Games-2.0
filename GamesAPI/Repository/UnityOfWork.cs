using GamesAPI.Context;
using Microsoft.Extensions.Logging;

namespace GamesAPI.Repository
{
    public class UnityOfWork : IUnitOfWork
    {
        private JogoRepository _jogoRepository;
        private CategoriaRepository _categoriaRepository;
        private AppDbContext _context;
        private readonly ILogger<CategoriaRepository> _categoriaLogger;
        private readonly ILogger<JogoRepository> _jogoLogger;

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                _categoriaLogger.LogInformation("Acessando Repositório de Categorias...");
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context, _categoriaLogger);
            }
        }

        public IJogoRepository JogoRepository
        {
            get
            {
                _jogoLogger.LogInformation("Acessando Repositório de Jogos...");
                return _jogoRepository = _jogoRepository ?? new JogoRepository(_context, _jogoLogger);
            }
        }

        public UnityOfWork(AppDbContext context, ILogger<CategoriaRepository> categoriaLogger, ILogger<JogoRepository> jogoLogger)
        {
            _context = context;
            _categoriaLogger = categoriaLogger;
            _jogoLogger = jogoLogger;
        }

        public async Task Commit()
        {
            _categoriaLogger.LogInformation("Salvando dados...");
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
