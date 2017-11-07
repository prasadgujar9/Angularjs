using OnlineShopping.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private OnlineShoppingDbContext dbContext;

        public OnlineShoppingDbContext Init()
        {
            return dbContext ?? (dbContext = new OnlineShoppingDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
