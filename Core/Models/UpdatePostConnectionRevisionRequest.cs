namespace WebApi.Models
{
    public record UpdatePostConnectionRevisionRequest
    {
        public int[] PostConnectionIds { get; init; }
        public UpdatePostConnectionRevisionRequest(int[] postConnectionIds)
        {
            PostConnectionIds = postConnectionIds;
        }
    }
}
