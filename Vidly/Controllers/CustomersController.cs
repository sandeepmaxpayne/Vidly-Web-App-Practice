using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController: Controller
    {
      
        public ViewResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }

      
        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.CustomerId == id);
            
            if(customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer { CustomerId = 1, CustomerName = "John Smith"},
                new Customer{CustomerId = 2, CustomerName = "Mary Williams"}
            };
        }
    }
}