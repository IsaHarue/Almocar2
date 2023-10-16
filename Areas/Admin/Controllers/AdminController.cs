using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Almocar2.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index(){
            return View();
        }
    }
}