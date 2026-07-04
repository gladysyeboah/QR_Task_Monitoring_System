using System.Collections.Generic;
using QR_Field_Monitoring_System.Models;

namespace QR_Field_Monitoring_System.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalTasks { get; set; }
        public int PendingTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int ActiveWorkersCount { get; set; }
        public int TotalWorkersCount { get; set; }

        public List<FieldTask> RecentTasks { get; set; } = new List<FieldTask>();
        public List<Submission> LatestSubmissions { get; set; } = new List<Submission>();
        public Dictionary<string, string> WorkerNames { get; set; } = new Dictionary<string, string>();
    }
}
