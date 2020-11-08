using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
       protected override void Dispose(bool disposing)
       {
            _context.Dispose();
       }
        
        public ViewResult Index()
        {

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
          //  return View();
        }


        public ActionResult Details(int id)
        {
            var view = _context.Movies.SingleOrDefault(m => m.Id == id);
            return View(view);
        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var movieFormModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", movieFormModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewMovieModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewMovieModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
               // movieInDb.Name = movie.Name;
                //movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleasedDate = movie.ReleasedDate;
                movieInDb.AmountInStock = movie.AmountInStock;
            }
            _context.SaveChanges();
          
          return  RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                return HttpNotFound();
            }
            else
            {
                var movieToEdit = new MovieFormViewModel(movieInDb)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", movieToEdit);
            }
            
        }










































        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() {Name="Shrek!" };
            var customers = new List<Customer>()
            {
                new Customer(){Name="Mosh Hamedani", Id=1},
                new Customer{Name="Kokummo Bamgbose", Id=2}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movies = movie,
                Customers = customers
            };
            return View(viewModel);

        }

    //    public ActionResult Edit(int id)
    //    {
    //        return Content("id= " + id);
    //    }

    //    [Route("movies/released /{year}/{month}")]
    //    public ActionResult ByReleasedDate(int year, int month)
    //    {

    //        return Content(year+"/"+month);
    //    }
        
    }
}