using System.ComponentModel.DataAnnotations;

namespace GroupMailer.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string HashPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<EmailCampaign> EmailCampaigns { get; set; } = new List<EmailCampaign>();
    }
}
