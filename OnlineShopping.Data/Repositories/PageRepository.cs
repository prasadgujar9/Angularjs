﻿using OnlineShopping.Data.Infrastructure;
using OnlineShopping.Model.Models;

namespace OnlineShopping.Data.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
    }

    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}