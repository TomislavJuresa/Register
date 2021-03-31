
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Store.Domain.Models
{
    public class Account:DomainObject
    {
        

        [Required(ErrorMessage = "Can not have an accout without owner")]
        public Employee Employee{ get; set; }
        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }
        [Required]
        public int CreatedByAccountID { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Please use small and capital letter and numbers")]
        public string Password { get; set; }
        [Required(ErrorMessage = "DisplayUsername is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Please use small and capital letter and numbers")]
        public string DisplayUsername { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
