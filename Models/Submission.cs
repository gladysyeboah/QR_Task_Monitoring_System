using System.ComponentModel.DataAnnotations;
using QR_Field_Monitoring_System.Models.Enums;

namespace QR_Field_Monitoring_System.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int FieldTaskId { get; set; }

        public FieldTask? FieldTask { get; set; }

        [Required]
        public string WorkerId { get; set; } = string.Empty;

        public ConditionStatus Condition { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public string? Comments { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}