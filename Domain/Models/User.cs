using Domain.Enums;

namespace Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int ProfileImageId { get; set; }
        public Image ProfileImage { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
