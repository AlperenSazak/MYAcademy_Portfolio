using System.ComponentModel.DataAnnotations;

namespace MYAcademy_Portfolio.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Başlık yazısı zorunludur.")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Hakkımda yazısı zorunludur.")]
        [StringLength(3000)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
