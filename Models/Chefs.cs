using System.ComponentModel.DataAnnotations.Schema;

namespace DeliciousMvC.Models
{
    public class Chefs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Work { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile Images { get; set; }
    }
}
