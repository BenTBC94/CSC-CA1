using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string Category { get; set; }

        [Range(0.10, 9999)]
        public decimal Price { get; set; }
    }
}