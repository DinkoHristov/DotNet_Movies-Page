using Microsoft.EntityFrameworkCore;
using MoviesPage.Data.Models;

namespace MoviesPage.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
            
        }

        public MovieDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MovieActor> MoviesActors { get; set; }

        public DbSet<MovieGenre> MoviesGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
