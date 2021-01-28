﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ModelView;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var movie = GetMovie();
            return View(movie);
        }

        private IEnumerable<Movie> GetMovie()
        {
            return new List<Movie>() { new Movie { Id = 1, Name = "Shrek" }, new Movie { Id = 2, Name = "Wall-e" } };
        }







        // GET: Movies/Random
        // ActionResult <Any Sub Type> or ViewResult<view>
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Wall-e" };
            // return View(movie);
            // return Content("Hello World");
            //  return HttpNotFound();
            // return new EmptyResult();
            // return RedirectToAction("Index", "Home", new { page = 1, SortBy = "name" })

            var customers = new List<Customer> { new Customer { CustomerName = "Customer 1" }, new Customer { CustomerName = "Customer 2" } };
            RandomViewModel randomModel = new RandomViewModel() { Movies = movie, Customers = customers };

            return View(randomModel);
        }
        public ActionResult Edit(int id)
        {
            return Content($"id = {id}");
                
        }
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            return Content($"PageNo:{pageIndex}&sortBy:{sortBy}");
        }

        [Route(@"movies/released/{year}/{month:regex(\d{2}):range(1,12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

      
    }
}