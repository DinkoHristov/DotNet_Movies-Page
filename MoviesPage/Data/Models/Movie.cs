namespace MoviesPage.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            Cast = new HashSet<MovieActor>();
            Genres = new HashSet<MovieGenre>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public virtual ICollection<MovieActor> Cast { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }
    }
}
