using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsSubscribedToCustomer { get; set; }
        public DateTime? BirthDate { get; set; }

        //Add navigation property with members
        public MembershipType MembershipType { get; set; }
        //Add foreign key
        public byte MembershipTypeId { get; set; }

    }
}