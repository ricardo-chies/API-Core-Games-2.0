namespace GamesAPI.Repository
{
    public interface IUnitOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IJogoRepository JogoRepository { get; }
        Task Commit();
    }
}
