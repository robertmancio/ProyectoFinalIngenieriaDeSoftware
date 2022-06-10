using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class AssignationVM
    {
        public Assignation Assignation { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationUserList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RequestList { get; set; }

    }
}
