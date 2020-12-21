using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            var allobj = _db.companies.FirstOrDefault(s => s.Id == company.Id);
            if (allobj != null)
            {
                allobj.Name = company.Name;
                allobj.StreetAddress = company.StreetAddress;
                allobj.State = company.State;
                allobj.City = company.City;
                allobj.PostalCode = company.PostalCode;
                allobj.PhoneNumber = company.PhoneNumber;
                allobj.IsAuthorizedCompany = company.IsAuthorizedCompany;

            }
        }
    }
}
