namespace QR_Field_Monitoring_System.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public int SubmissionId { get; set; }

        public Submission? Submission { get; set; }
    }
}