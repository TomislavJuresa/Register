﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Store.Domain.Models
{
    public class Product : DomainObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^\\d{0,8}(\\.\\d{1,2})?$", ErrorMessage = "Must be a valid price, for example 11.22")]

        public double Price { get; set; }
    //    [ForeignKey("Vendor")]
    public Vendor Vendor { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
