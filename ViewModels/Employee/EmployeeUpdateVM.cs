using Anyar.Models;

namespace Anyar.ViewModels
{
    public class EmployeeUpdateVM
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public IFormFile? Image { get; set; }
        public string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkEdinLink { get; set; }
        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
