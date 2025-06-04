using System.ComponentModel.DataAnnotations;

namespace ep_back_end.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public string Role { get; set; } = "User";  // optional: Admin, etc.
    }

}
