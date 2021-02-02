using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.Vidly.Models;
using Vidly.ModelView;

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
            //  var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //     return View(customers);
            return View();
        }

        public ActionResult New()
        {
            var memeberList = _context.MembershipTypes.ToList();
            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = memeberList
               
            };

            return View("CustomerForm" ,viewModel);
        }

        //Model Binding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }


            if(customer.CustomerId == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.CustomerId == customer.CustomerId);
                /*Using AutoMapper Package
                 * Mapper.Map("customer", customerInDb);
                 */
                customerInDb.CustomerName = customer.CustomerName;
                customerInDb.BirthDate = customer.BirthDate;
                customer.MembershipTypeId = customer.MembershipTypeId;
                customer.IsSubscribedToCustomer = customer.IsSubscribedToCustomer;
            
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        //Edit Customer Action
        //this id is a hidden field declared in CustomerForm before submitting
        public ActionResult EditForm(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                return HttpNotFound("No Customer Found!");
            }
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
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