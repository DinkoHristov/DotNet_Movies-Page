using Microsoft.AspNetCore.Mvc;
using MoviesPage.Data;
using MoviesPage.Data.Models;
using MoviesPage.Models;

namespace MoviesPage.Controllers
{
	public class CreateMovieController : Controller
    {
		private readonly MovieDbContext context;

		public CreateMovieController(MovieDbContext dbContext)
		{
			context = dbContext;
		}

		public IActionResult Create()
        {
			var genres = context.Genres.ToList();

			ViewBag.Genres = genres;

			return View();
        }

        [HttpPost]
        public IActionResult OnPost(MovieViewModel movie)
        {
            var genres = context.Genres.ToList();

            ViewBag.Genres = genres;

            if (!ModelState.IsValid)
            {
                movie.ErrorMessage = "Data validation failed!";

                return View(nameof(Create), movie);
            }

            var movies = context.Movies.ToList();
            if (!movies.Any(m => m.Title.ToLower() == movie.Title.ToLower()))
            {
                var newMovie = new Movie()
                {
                    Title = movie.Title,
                    Year = (int)movie.Year,
                };

                var genre = context.Genres.FirstOrDefault(g => g.Id == int.Parse(movie.Genre));
                var movieGenre = new MovieGenre()
                {
                    Genre = genre,
                    Movie = newMovie
                };

                newMovie.Genres.Add(movieGenre);
                context.Movies.Add(newMovie);
                context.SaveChanges();

				movie.SuccessfullMessage = "Your movie was created successfully!";
			}
            else
            {
                movie.MovieAlreadyExists = "This movie already exists!";
            }

            movie.Title = "";
            movie.Year = null;
			ModelState.Clear();

            return View(nameof(Create), movie);
        }
    }
}
