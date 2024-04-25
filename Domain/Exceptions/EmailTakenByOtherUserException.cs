namespace Domain.Exceptions
{
    public class EmailTakenByOtherUserException : BadRequestException
    {
        public EmailTakenByOtherUserException(string userEmail)
            : base($"Email {userEmail} is already taken by other user")
        {
        }
    }
}
