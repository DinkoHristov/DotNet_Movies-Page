namespace MoviesPage.Data.Models
{
    public class Actor
    {
        public Actor()
        {
            Movies = new HashSet<MovieActor>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}
