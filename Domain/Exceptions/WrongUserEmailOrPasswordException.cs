namespace Domain.Exceptions
{
    public class WrongUserEmailOrPasswordException : BadRequestException
    {
        public WrongUserEmailOrPasswordException(string? message = null) : base(message ?? "Wrong email or password")
        {

        }
    }
}
