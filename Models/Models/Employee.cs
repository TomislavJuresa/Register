using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Models
{
    public class Employee :DomainObject
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Must be between 2 and 100 characters", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please use small and capital letter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100, ErrorMessage = "Must be between 2 and 100 characters", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Please use small and capital letter")]
        public string Surname { get; set; }
      
        public string Adress { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        [Required(ErrorMessage = "Superveiser is required")]
        public int ReportsToEmployeeID { get; set; }

    }
}
