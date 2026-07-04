using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QR_Field_Monitoring_System.Data;
using QR_Field_Monitoring_System.Models;
using QR_Field_Monitoring_System.Models.Enums;
using QR_Field_Monitoring_System.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QR_Field_Monitoring_System.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var totalTasks = await _context.FieldTasks.CountAsync();
            var pendingTasks = await _context.FieldTasks.CountAsync(t => t.Status == FieldTaskStatus.Pending || t.Status == FieldTaskStatus.InProgress);
            var completedTasks = await _context.FieldTasks.CountAsync(t => t.Status == FieldTaskStatus.Completed);
            
            // Count registered workers
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            var totalWorkersCount = workers.Count;
            var activeWorkersCount = totalWorkersCount; // Treating all registered workers as active in the dashboard

            // Fetch the 5 most recently created tasks
            var recentTasks = await _context.FieldTasks
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToListAsync();

            // Fetch the 5 most recent submissions
            var latestSubmissions = await _context.Submissions
                .Include(s => s.FieldTask)
                .OrderByDescending(s => s.SubmittedAt)
                .Take(5)
                .ToListAsync();

            // Map worker IDs to display names (Full Name or Username)
            var allUsers = await _userManager.Users.ToListAsync();
            var workerNames = allUsers.ToDictionary(u => u.Id, u => !string.IsNullOrWhiteSpace(u.FullName) ? u.FullName : (u.UserName ?? "Unknown"));

            var viewModel = new DashboardViewModel
            {
                TotalTasks = totalTasks,
                PendingTasks = pendingTasks,
                CompletedTasks = completedTasks,
                ActiveWorkersCount = activeWorkersCount,
                TotalWorkersCount = totalWorkersCount,
                RecentTasks = recentTasks,
                LatestSubmissions = latestSubmissions,
                WorkerNames = workerNames
            };

            return View(viewModel);
        }
    }
}