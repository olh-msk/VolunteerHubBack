namespace Domain.Exceptions
{
    public class TagNotFoundException : BadRequestException
    {
        public TagNotFoundException(int tagId) : base($"Tag with id = {tagId} was not found")
        {
        }
    }
}
