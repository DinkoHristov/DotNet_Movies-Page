using MoviesPage.Data;
using MoviesPage.Data.Models;
using MoviesPage.DataInitialization.DataInitialization.ImportModels;
using System.Text.Json;

namespace MoviesPage.DataInitialization
{
    public class DataFeed
    {
        private static MovieDbContext context;

        public DataFeed(MovieDbContext dbContext)
        {
            context = dbContext;
        }

        public void Initialize()
        {
            var movieDtos = (List<MovieDto>)JsonSerializer.Deserialize(File.ReadAllText("DataInitialization/movies-2020s.json"), typeof(List<MovieDto>));

            foreach (var movieDto in movieDtos)
            {
                if (!string.IsNullOrEmpty(movieDto.Title) &&
                    movieDto.Year != null)
                {
                    var movie = new Movie
                    {
                        Title = movieDto.Title,
                        Year = (int)movieDto.Year,
                    };

                    foreach (var actorName in movieDto.CastNames.Distinct())
                    {
                        if (!string.IsNullOrEmpty(actorName))
                        {
                            if (!context.Actors.Any(a => a.Name == actorName))
                            {
                                var actor = new Actor() { Name = actorName };
                                context.Actors.Add(actor);
                            }

                            context.SaveChanges();

                            var movieActor = new MovieActor()
                            {
                                Actor = context.Actors.FirstOrDefault(a => a.Name == actorName)
                            };

                            movie.Cast.Add(movieActor);
                        }
                    }

                    foreach (var genreType in movieDto.Genres.Distinct())
                    {
                        if (!string.IsNullOrEmpty(genreType))
                        {
                            if (!context.Genres.Any(m => m.Name == genreType))
                            {
                                var genre = new Genre() { Name = genreType};
                                context.Genres.Add(genre);
                            }

                            context.SaveChanges();

                            var movieGenre = new MovieGenre()
                            {
                                Genre = context.Genres.FirstOrDefault(g => g.Name == genreType)
                            };

                            movie.Genres.Add(movieGenre);
                        }
                    }

                    context.Movies.Add(movie);
                }
            }

            context.SaveChanges();
        }
    }
}
