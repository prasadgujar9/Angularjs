namespace OnlineShopping.Data.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}