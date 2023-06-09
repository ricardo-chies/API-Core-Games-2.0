using GamesAPI.Context;

namespace GamesAPI.Repository
{
    public class UnityOfWork : IUnitOfWork
    {
        private JogoRepository _jogoRepository;
        private CategoriaRepository _categoriaRepository;
        private AppDbContext _context;
        public ICategoriaRepository CategoriaRepository
        {
            get 
            { 
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public IJogoRepository JogoRepository
        {
            get
            {
                return _jogoRepository = _jogoRepository ?? new JogoRepository(_context);
            }
        }

        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
