using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            SP_Call = new SP_Call(_db);
            User = new UserRepository(_db);
            orderHeader = new OrderHeaderRepository(_db);
            orderDetails = new OrderDetailsRepository(_db);
            shoppingCart = new ShoppingCartRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public ICompanyRepository Company { get; private set; }
       

        public IUserRepository User { get; private set; }

        public IOrderDetailsRepository orderDetails   { get; private set; }

        public IOrderHeaderRepository orderHeader { get; private set; }

        public IShoppingCartRepository shoppingCart { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
