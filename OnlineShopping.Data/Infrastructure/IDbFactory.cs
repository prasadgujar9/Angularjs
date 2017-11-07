using System;

namespace OnlineShopping.Data.Repositories
{
    public interface IDbFactory : IDisposable
    {
        OnlineShoppingDbContext Init();
    }
}