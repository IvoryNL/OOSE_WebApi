namespace WebAPI.Models
{
    public class HashedPasswordModel
    {
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
