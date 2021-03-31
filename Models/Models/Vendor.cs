
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Store.Domain.Models
{
    public class Vendor : DomainObject
    {
        public string CompanyName { get; set; }
        public string Adress { get; set; } //TODO: make Adress Class
        public DateTime CreatedOn { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
