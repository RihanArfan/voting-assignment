using Web.Models;

namespace Web.Controllers;

public class ReportsController : Controller
{
    private readonly IElectionService _electionService;
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService, IElectionService electionService)
    {
        _reportService = reportService;
        _electionService = electionService;
    }

    public async Task<IActionResult> Index()
    {
        var elections = await _electionService.GetAllAsync();
        return View(elections);
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var election = await _electionService.GetAsync(id);

        // if election doesn't exist, show 404
        if (election == null) return NotFound();

        var timeSeriesVotes = _reportService.GetTimeSeriesVotes(id);
        var partyVotes = _reportService.GetPartyVotes(id);
        var totalVotes = await _reportService.GetVoteCount(id);

        var reportViewModel = new ReportViewModel
        {
            Election = election,
            TimeSeriesVotes = timeSeriesVotes,
            PartyVotes = partyVotes,
            TotalVotes = totalVotes
        };

        return View(reportViewModel);
    }
}