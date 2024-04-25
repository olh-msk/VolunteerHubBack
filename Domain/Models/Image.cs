namespace Domain.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Format { get; set; }

        public override string? ToString()
        {
            return $"{ImageId}.{Format}";
        }
    }
}
