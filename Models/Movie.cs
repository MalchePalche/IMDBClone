using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public string Director { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public string PosterUrl { get; set; } = string.Empty;

        public string Actors { get; set; } = string.Empty; // comma-separated names
        public string TrailerUrl { get; set; } = string.Empty; // YouTube embed or link


    }
}
