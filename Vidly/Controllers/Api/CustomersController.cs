using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.App_Start;
using System.Data.Entity;
using Vidly.Dtos;
using Vidly.Models;
using Vidly.Models.Vidly.Models;

namespace Vidly.Controllers.Api
{
 
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //By convention GetCustomers is GET /api/Customers
        public IEnumerable<CustomerDto> GetCustomers(string query = null)
        {

            var customerQuery = _context.Customers
                .Include(c => c.MembershipType);

            if(!String.IsNullOrWhiteSpace(query))
            {
                customerQuery = customerQuery.Where(c => c.CustomerName.Contains(query));
            }

            var customerDtos = customerQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return customerDtos;
        }

        //Get Customer by ID => GET /api/Custoemrs/1
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer =  _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST  api/Customers
        //Since we are creating a resource use [HttpPost]
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.CustomerId = customer.CustomerId;
            return Created(new Uri($"{Request.RequestUri}/{customer.CustomerId}"), customerDto);
        }

        //PUT /api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customerDb = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if(customerDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Mapper.Map(customerDto, customerDb);

            _context.SaveChanges();
        } 

        // Delete Customer /api/Customer/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerDb = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if(customerDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(customerDb);
            _context.SaveChanges();
        }

    }
}
