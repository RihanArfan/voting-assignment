namespace Web.Controllers;

public class ReportController : Controller
{
    private ElectionService _electionService;
    private ReportService _reportService;

    private ReportController(ReportService reportService, ElectionService electionService)
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