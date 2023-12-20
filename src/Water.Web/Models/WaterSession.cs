using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Water.Web.Models
{
    public class WaterSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Owner { get; set; } = null!;

        [MaxLength(256)]
        public string? WithText { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public TimeSpan Dealline { get; set; }

        [Required]
        public WaterSessionState State { get; set; }

        public virtual ICollection<WaterSessionItem> Items { get; set; }
    }
}
