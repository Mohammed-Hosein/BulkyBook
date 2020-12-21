using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        IUserRepository User { get; }

        ISP_Call SP_Call { get; }

        IShoppingCartRepository shoppingCart {get;}

        IOrderDetailsRepository orderDetails { get; }

        IOrderHeaderRepository orderHeader { get; }

        

        void Save();
    }
}
