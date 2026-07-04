using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QR_Field_Monitoring_System.Data;
using QR_Field_Monitoring_System.Models;
using QR_Field_Monitoring_System.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QR_Field_Monitoring_System.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string search, FieldTaskStatus? status, PriorityLevel? priority)
        {
            var query = _context.FieldTasks
                .Include(t => t.AssignedWorker)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.ToLower().Trim();
                query = query.Where(t => t.Title.ToLower().Contains(searchLower)
                                      || t.Description.ToLower().Contains(searchLower)
                                      || t.AssetName.ToLower().Contains(searchLower)
                                      || t.Location.ToLower().Contains(searchLower));
            }

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }

            var tasks = await query.OrderByDescending(t => t.CreatedAt).ToListAsync();

            ViewData["Search"] = search;
            ViewData["StatusFilter"] = status;
            ViewData["PriorityFilter"] = priority;

            return View(tasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.FieldTasks
                .Include(t => t.AssignedWorker)
                .Include(t => t.Submissions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public async Task<IActionResult> Create()
        {
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            ViewBag.Workers = workers;
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,AssetName,Location,Priority,DueDate,AssignedWorkerId")] FieldTask fieldTask)
        {
            if (ModelState.IsValid)
            {
                fieldTask.CreatedAt = DateTime.Now;
                fieldTask.Status = FieldTaskStatus.Pending;
                _context.Add(fieldTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            ViewBag.Workers = workers;
            return View(fieldTask);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldTask = await _context.FieldTasks.FindAsync(id);
            if (fieldTask == null)
            {
                return NotFound();
            }
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            ViewBag.Workers = workers;
            return View(fieldTask);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,AssetName,Location,Priority,Status,DueDate,AssignedWorkerId")] FieldTask fieldTask)
        {
            if (id != fieldTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.FieldTasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                    if (existing != null)
                    {
                        fieldTask.CreatedAt = existing.CreatedAt;
                    }
                    _context.Update(fieldTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldTaskExists(fieldTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            ViewBag.Workers = workers;
            return View(fieldTask);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldTask = await _context.FieldTasks
                .Include(t => t.AssignedWorker)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (fieldTask == null)
            {
                return NotFound();
            }

            return View(fieldTask);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fieldTask = await _context.FieldTasks.FindAsync(id);
            if (fieldTask != null)
            {
                _context.FieldTasks.Remove(fieldTask);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FieldTaskExists(int id)
        {
            return _context.FieldTasks.Any(e => e.Id == id);
        }
    }
}
