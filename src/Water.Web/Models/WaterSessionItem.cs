using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Water.Web.Models
{
    public class WaterSessionItem
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Session))]
        public uint SessionId { get; set; }

        public virtual WaterSession? Session { get; set; }
    }
}
