namespace Web.Controllers;

public class ReportsController : Controller
{
    private ElectionService _electionService;
    private ReportService _reportService;

    private ReportsController(ReportService reportService, ElectionService electionService)
    {
        _reportService = reportService;
        _electionService = electionService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var elections = await _electionService.GetAll();
        return View(elections);
    }
}