namespace Domain.Exceptions
{
    public class PostException : BadRequestException
    {
        public PostException(string message) : base(message)
        {
        }
    }
}
