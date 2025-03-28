using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDBClone.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
