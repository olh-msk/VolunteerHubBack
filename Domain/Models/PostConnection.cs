namespace Domain.Models
{
    public class PostConnection
    {
        public int PostConnectionId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Post VolunteerPost { get; set; }
        public Post NeedfulPost { get; set; }
        public int SenderId { get; set; }
        public bool SenderHasSeen { get; set; }
        public bool ReceiverHasSeen { get; set; }

    }
}
