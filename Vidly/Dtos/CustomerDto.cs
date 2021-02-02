using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer's Name Required")]
        [StringLength(255)]
        public string CustomerName { get; set; }

        public bool IsSubscribedToCustomer { get; set; }

      //  [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        //Add foreign key
        [Required(ErrorMessage = "Member Type Required")]
        public byte MembershipTypeId { get; set; }
    }
}