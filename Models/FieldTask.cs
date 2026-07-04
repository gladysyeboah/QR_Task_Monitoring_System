using System.ComponentModel.DataAnnotations;
using QR_Field_Monitoring_System.Models.Enums;

namespace QR_Field_Monitoring_System.Models
{
    public class FieldTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string AssetName { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        public PriorityLevel Priority { get; set; }

        public DateTime DueDate { get; set; }

        public FieldTaskStatus Status { get; set; } = FieldTaskStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}