using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Models
{
    public class Shelf:DomainObject
    {
        [Required]
        public int Row { get; set; }
        [Required]
        public int Column { get; set; }

        public List<Compartment> Compartments { get; set; } = new List<Compartment>();
    }
}
