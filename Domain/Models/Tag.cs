using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required(ErrorMessage = "Name is reqired")]
        [MinLength(2, ErrorMessage = "Name must contains at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters long")]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
