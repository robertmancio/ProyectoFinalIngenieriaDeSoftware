using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class AssignationRepository : Repository<Assignation>, IAssignationRepository
    {
        private ApplicationDbContext _db;
        public AssignationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }


        public void Update(Assignation obj)
        {
            _db.Assignations.Update(obj);
        }
    }
}
