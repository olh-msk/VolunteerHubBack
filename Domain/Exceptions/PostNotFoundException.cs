namespace Domain.Exceptions
{
    public class PostNotFoundException : PostException
    {
        public PostNotFoundException(string postId) : base($"Post with id = {postId} was not found")
        {

        }
    }
}
