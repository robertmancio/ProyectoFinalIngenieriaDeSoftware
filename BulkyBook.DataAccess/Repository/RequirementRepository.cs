using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        private ApplicationDbContext _db;
        public RequirementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }


        public void Update(Requirement obj)
        {
            _db.Requirements.Update(obj);
        }
    }
}
