using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PostConnection> PostConnections { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Житло" },
                new Tag { TagId = 2, Name = "Медицина" });

            modelBuilder.Entity<Image>().HasData(
                new Image { ImageId = 1, Format = "jpg" },
                new Image { ImageId = 2, Format = "jpg" },
                new Image { ImageId = 3, Format = "jpg" });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Admin",
                    Surname = "Test",
                    Email = "admin@example.com",
                    PhoneNumber = "88005553535",
                    Address = "Admin street",
                    Role = Domain.Enums.UserRole.Admin,
                    ProfileImageId = 1,
                    Password = new byte[64] { 218, 49, 142, 22, 217, 252, 247, 124, 109, 0, 230, 127, 13, 159, 153, 241, 198, 18, 231, 211, 121, 153, 219, 80, 72, 249, 255, 91, 137, 192, 210, 60, 237, 223, 205, 159, 252, 193, 17, 250, 226, 239, 169, 34, 40, 168, 23, 154, 1, 18, 145, 73, 83, 35, 91, 153, 4, 16, 72, 77, 43, 55, 31, 66 }
                },
                new User
                {
                    UserId = 2,
                    Name = "Volunteer",
                    Surname = "Test",
                    Email = "volunteer@example.com",
                    PhoneNumber = "88005553535",
                    Address = "Volunteer street",
                    Role = Domain.Enums.UserRole.Volunteer,
                    ProfileImageId = 2,
                    Password = new byte[64] { 99, 75, 46, 160, 118, 173, 166, 203, 149, 66, 168, 76, 228, 217, 62, 28, 122, 239, 119, 97, 193, 144, 144, 174, 126, 40, 85, 241, 126, 172, 52, 124, 193, 254, 164, 70, 89, 111, 57, 116, 123, 166, 87, 127, 182, 160, 34, 229, 49, 74, 86, 30, 159, 87, 242, 34, 173, 108, 90, 106, 103, 22, 75, 104 }
                },
                new User
                {
                    UserId = 3,
                    Name = "Needful",
                    Surname = "Test",
                    Email = "needful@example.com",
                    PhoneNumber = "88005553535",
                    Address = "Needful street",
                    Role = Domain.Enums.UserRole.Needful,
                    ProfileImageId = 3,
                    Password = new byte[64] { 27, 212, 237, 60, 65, 101, 16, 189, 249, 137, 227, 146, 38, 28, 79, 23, 79, 224, 126, 171, 1, 61, 77, 13, 88, 35, 116, 198, 177, 117, 213, 74, 181, 201, 67, 124, 241, 109, 76, 40, 253, 125, 122, 90, 238, 171, 83, 163, 68, 178, 141, 242, 133, 196, 176, 7, 207, 13, 137, 162, 94, 122, 182, 234 }
                });

            modelBuilder.Entity<PostConnection>()
                .HasOne(p => p.VolunteerPost)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PostConnection>()
                .HasOne(p => p.NeedfulPost)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(p => p.ProfileImage)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
               .HasOne(p => p.PostImage)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(tag => tag.Name)
                .IsUnique();
        }
    }
}
