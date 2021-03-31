using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Domain.Models
{
    public class Compartment:DomainObject
    {
        //Todo: Coordinates in Shelf
        [Required]
        public int MinNumberOfProducts { get; set; }
        [Required]
        public int MaxNumberOfProducts { get; set; }
        public int CurrentNumberOfProducts { get; set; }

        public Product Product { get; set; }

    }
}