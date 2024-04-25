namespace Domain.Exceptions
{
    public class PostConnectionNotFoundException : BadRequestException
    {
        public PostConnectionNotFoundException(string postConnectionId) : base($"Post connection with id = {postConnectionId} was not found")
        {

        }
    }
}
