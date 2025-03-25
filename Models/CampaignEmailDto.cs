namespace GroupMailer.Models
{
    public class CampaignEmailDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public int TargetGroupId { get; set; }
        public int UserId { get; set; }
    }
}
