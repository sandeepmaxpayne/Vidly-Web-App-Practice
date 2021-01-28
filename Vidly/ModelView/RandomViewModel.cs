using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ModelView
{
    public class RandomViewModel
    {
        public Movie Movies { get; set; }
        public List<Customer> Customers { get; set; }
    }
}