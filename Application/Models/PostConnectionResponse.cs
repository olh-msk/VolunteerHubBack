using Domain.Models;

namespace Application.Models
{
    public record PostConnectionResponse
    {
        public int PostConnectionId { get; init; }
        public string Header { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
        public Post VolunteerPost { get; init; }
        public Post NeedfulPost { get; init; }
        public bool UserHasSeen { get; init; }
        public PostConnectionResponse(int postConnectionId, string header, string title, string message, Post volunteerPost, Post needfulPost, bool userHasSeen)
        {
            PostConnectionId = postConnectionId;
            Header = header;
            Title = title;
            Message = message;
            VolunteerPost = volunteerPost;
            NeedfulPost = needfulPost;
            UserHasSeen = userHasSeen;
        }
    }
}
