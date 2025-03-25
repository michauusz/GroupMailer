using GroupMailer.Enums;

namespace GroupMailer.Models
{
    public class EmailCampaign
    {
        public int Id { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public CampaignStatus Status { get; set; } = CampaignStatus.UnInitialized;
        public List<int> TargetGroupsId { get; set; } = new List<int>();
        public List<string> EmailList { get; set; } = new List<string>();
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
