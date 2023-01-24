using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string UsernameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set; } = false;
    }
}
