using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }
        
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        [Required(ErrorMessage ="Movie is Required")]
        public Movie Movie { get; set; }

        [Required(ErrorMessage = "Customer is Required")]
        public Customer Customer { get; set; }
    }
}