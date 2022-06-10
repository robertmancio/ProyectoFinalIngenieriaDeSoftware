using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Assignation = new AssignationRepository(_db);
            Requirement = new RequirementRepository(_db);
            Request = new RequestRepository(_db);
            ApplicationUser =new ApplicationUserRepository(_db);
        }

        public IAssignationRepository Assignation { get; private set; }
        public IRequirementRepository Requirement { get; private set; }
        public IRequestRepository Request { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
