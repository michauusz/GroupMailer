using System.ComponentModel.DataAnnotations;

namespace GroupMailer.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? CustomName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
