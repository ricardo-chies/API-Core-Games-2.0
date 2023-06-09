namespace GamesAPI.Repository
{
    public interface IUnitOfWork
    {
        ICategoriaRepository CategoriaRepository { get; }
        IJogoRepository JogoRepository { get; }
        void Commit();
    }
}
