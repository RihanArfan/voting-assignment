namespace Web.Areas.Management.Controllers;

[Area("Management")]
// [Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}