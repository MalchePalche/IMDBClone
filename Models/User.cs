using System.ComponentModel.DataAnnotations;

namespace IMDBClone.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // Default role

        public ICollection<Review> Reviews { get; set; }
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; }
    }
}
