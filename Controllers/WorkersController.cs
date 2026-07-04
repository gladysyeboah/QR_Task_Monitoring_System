using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QR_Field_Monitoring_System.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
