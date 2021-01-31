using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.Vidly.Models;
using Vidly.ModelView;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public ViewResult Index()
        {
            var movie = _context.Movies.Include(m => m.Genre).ToList();
            return View(movie);
        }

        /*private IEnumerable<Movie> GetMovie()
        {
            return new List<Movie>() { new Movie { Id = 1, Name = "Shrek" }, new Movie { Id = 2, Name = "Wall-e" } };
        }*/

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            
            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ViewResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MoviesFormViewModel 
            {
                Genres = genres, 
                Movie = new Movie {ReleaseDate = new DateTime(), Stock = 0
                }
            };
            return View("NewMovieForm", viewModel);
        }

        public ActionResult EditMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                HttpNotFound("Sorry No movie is available");
            }

            var viewModel = new MoviesFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("NewMovieForm", viewModel);
        }

        //saveMovieForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMovie(Movie movie)
        {

            //validate the page
            if (!ModelState.IsValid)
            {
                var viewModel = new MoviesFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("NewMovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieDb.Name = movie.Name;
                movieDb.GenreId = movie.GenreId;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.Stock = movie.Stock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
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