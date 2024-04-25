namespace Application.Services
{
    public interface IHashingService
    {
        public byte[] GetHash(string password);
    }
}
