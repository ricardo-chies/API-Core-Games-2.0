using GamesAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GamesAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(AppDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<T> Get()
        {
            _logger.LogInformation("Buscando informações...");
            return _context.Set<T>().AsNoTracking(); //desabilito o rastreamento de entidades e ganho desempenho
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation("Buscando informações por ID...");
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _logger.LogInformation("Inserindo novos dados...");
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _logger.LogInformation("Atualizando dados...");
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _logger.LogInformation("Deletando dados...");
            _context.Set<T>().Remove(entity);
        }  
    }
}
