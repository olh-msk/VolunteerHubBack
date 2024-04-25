namespace Domain.Exceptions
{
    public class WrongPostTypeException : PostException
    {
        public WrongPostTypeException(string postId, string actualType, string expectedType)
            : base($"Wrong type of post with id = {postId}. Type was {actualType} but expected {expectedType}")
        {

        }
    }
}
