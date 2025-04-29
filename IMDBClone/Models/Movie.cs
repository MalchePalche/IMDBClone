using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }

        // Optional fields
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }

        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string PosterUrl { get; set; } = string.Empty;
        public string Actors { get; set; } = string.Empty;
        public string TrailerUrl { get; set; } = string.Empty;
    }
}
