using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController: Controller
    {

        //use applicationdbcontext to query the data from database
        private ApplicationDbContext _context = new ApplicationDbContext();


        // DbContext is a disposable object
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

      
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.CustomerId == id);
            
            if(customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }
    }
}