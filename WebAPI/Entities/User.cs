using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] PasswordKey { get; set; }
    }
}
