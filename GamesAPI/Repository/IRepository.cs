using System.Linq.Expressions;

namespace GamesAPI.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(); //posso customizar depois essa consulta
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
