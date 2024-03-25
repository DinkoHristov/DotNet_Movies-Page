using Microsoft.AspNetCore.Mvc;
using MoviesPage.Data;
using MoviesPage.Data.Models;
using MoviesPage.Models;

namespace MoviesPage.Controllers
{
	public class DetailsMovieController : Controller
	{
		private readonly MovieDbContext context;
		private static MovieViewModel _editMovie;

		public DetailsMovieController(MovieDbContext dbContext)
		{
			context = dbContext;
		}

		public IActionResult Details()
		{
			var movies = GetAllMovies();

			return View(movies);
		}

		public IActionResult Select(MovieViewModel movie)
		{
			var movies = GetAllMovies();

			ViewData["SelectedMovie"] = movie;
			return View(nameof(Details), movies);
		}

		public IActionResult Edit(MovieViewModel movie)
		{
			var genres = context.Genres.ToList();

			ViewBag.Genres = genres;

			_editMovie = movie;

			return View(movie);
		}

		[HttpPost]
		public IActionResult OnEditPost(MovieViewModel movie)
		{
			var dbMovie = context.Movies.FirstOrDefault(m => m.Title == _editMovie.Title);
			var movieGenres = context.MoviesGenres.Where(mg => mg.MovieId == dbMovie.Id).ToList();

			if (ModelState.IsValid)
			{
				dbMovie.Title = movie.Title;
				dbMovie.Year = (int)movie.Year;
				dbMovie.Genres.Clear();
				movieGenres.Clear();

				var genre = context.Genres.FirstOrDefault(g => g.Id == int.Parse(movie.Genre));
				dbMovie.Genres.Add(new MovieGenre { Genre = genre, Movie = dbMovie});

				context.SaveChanges();

				var movies = GetAllMovies();

				return View(nameof(Details), movies);
			}

			var genres = context.Genres.ToList();

			ViewBag.Genres = genres;

			return View(nameof(Edit), movie);
		}

		public IActionResult Delete(MovieViewModel movie)
		{
			var dbMovie = context.Movies.FirstOrDefault(m => m.Title == movie.Title);
			context.Movies.Remove(dbMovie);

			context.SaveChanges();

			ViewData["DeletedMovie"] = movie;

			var movies = GetAllMovies();

			return View(nameof(Details), movies);
		}

		private List<MovieViewModel> GetAllMovies()
		{
			return context.Movies
				.Select(m => new MovieViewModel
				{
					Title = m.Title,
					Year = m.Year,
					Genre = m.Genres.Select(g => g.Genre.Name).FirstOrDefault()
				})
				.ToList();
		}
		
	}
}
