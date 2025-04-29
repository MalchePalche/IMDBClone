using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IMDBClone.Models
{
    public class User : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; } 
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; }
    }
}
