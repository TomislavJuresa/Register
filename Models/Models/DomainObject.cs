using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Domain.Models
{
    public class DomainObject
    {
        [Key]
        public int Id { get; set; }
    }
}
