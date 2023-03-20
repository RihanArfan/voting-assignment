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
        // get election
        var election = await _electionService.GetAsync(id);

        // if election doesn't exist, show 404
        if (election == null) return NotFound();

        // get report data for election
        var timeSeriesVotes = _reportService.GetTimeSeriesVotes(id);
        var partyVotes = _reportService.GetPartyVotes(id);
        var totalVotes = await _reportService.GetVoteCount(id);

        // create view model using data
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