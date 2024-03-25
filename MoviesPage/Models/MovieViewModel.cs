using System.ComponentModel.DataAnnotations;

namespace MoviesPage.Models
{
    public class MovieViewModel
    {
        public string ErrorMessage = string.Empty;

        public string SuccessfullMessage = string.Empty;

        public string MovieAlreadyExists = string.Empty;


		[Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }
    }
}
