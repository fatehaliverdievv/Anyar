using Anyar.Models.Base;

namespace Anyar.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl{ get; set; }
        public string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkEdinLink { get; set; }
        public int PositionId { get; set; } 
        public Position? Position { get; set; }
    }
}
