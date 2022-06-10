using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string Status { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementId { get; set; }
        [ValidateNever]
        public Requirement Requirement { get; set; }
    }
}
