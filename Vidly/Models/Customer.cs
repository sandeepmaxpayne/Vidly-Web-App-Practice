using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer's Name Required")]
        [StringLength(255)]
        public string CustomerName { get; set; }

        public bool IsSubscribedToCustomer { get; set; }

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        //Add navigation property with members
        public MembershipType MembershipType { get; set; }
        //Add foreign key
        [Required(ErrorMessage = "Member Type Required")]
        public byte MembershipTypeId { get; set; }

    }
}