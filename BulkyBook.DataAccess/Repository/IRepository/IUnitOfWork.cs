using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAssignationRepository Assignation { get; }
        IRequirementRepository Requirement { get; }
        IRequestRepository Request { get; }
        IApplicationUserRepository ApplicationUser { get; } 
        void Save(); 
    }
}
