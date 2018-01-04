using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class Talent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ShortName is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Reknown is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string Reknown { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [RegularExpression("^[A-Z].*", ErrorMessage = "First character must be an uppercase")]
        public string Bio { get; set; }

    }
}