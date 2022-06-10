using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        private ApplicationDbContext _db;
        public RequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }


        public void Update(Request obj)
        {
            _db.Requests.Update(obj);
        }
    }
}
