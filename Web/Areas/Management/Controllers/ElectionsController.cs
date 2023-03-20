using Web.Areas.Management.Models;

namespace Web.Areas.Management.Controllers;

[Area("Management")]
public class ElectionsController : Controller
{
    private readonly IElectionService _electionService;
    private readonly IPartyService _partyService;

    public ElectionsController(IElectionService electionService, IPartyService partyService)
    {
        _electionService = electionService;
        _partyService = partyService;
    }

    // GET: Elections
    public async Task<ActionResult> Index()
    {
        var elections = await _electionService.GetAllAsync();
        ViewBag.Success = Convert.ToBoolean(TempData["Success"] ?? false);
        return View(elections);
    }

    // GET: Elections/Create
    public async Task<ActionResult> CreateAsync()
    {
        // get required data
        var parties = await _partyService.GetAllAsync();

        // convert model to view model
        var electionViewModel = new ElectionViewModel
        {
            PartiesViewModel = parties.Select(p => new ElectionPartyViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Selected = false
            }).ToList()
        };

        TempData["Success"] = true; // show success message
        return View(electionViewModel);
    }

    // POST: Elections/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ElectionViewModel electionViewModel)
    {
        // handle invalid model state
        if (!ModelState.IsValid) return View();

        try
        {
            // convert view model to model
            var electionModel = new Election
            {
                Name = electionViewModel.Name,
                StartDate = electionViewModel.StartDate,
                EndDate = electionViewModel.EndDate,
                Parties = electionViewModel.PartiesViewModel.Where(p => p.Selected).Select(p => new Party { Id = p.Id })
                    .ToList()
            };

            // create election
            _electionService.CreateAsync(electionModel);

            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Elections/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        // get required data
        var election = await _electionService.GetAsync(id);
        var parties = await _partyService.GetAllAsync();

        // if election doesn't exist, show 404 page
        if (election == null) return NotFound();

        // convert model to view model
        var electionViewModel = new ElectionViewModel
        {
            Id = election.Id,
            Name = election.Name,
            StartDate = election.StartDate,
            EndDate = election.EndDate,
            PartiesViewModel = parties.Select(p => new ElectionPartyViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Selected = election.Parties.Any(ep => ep.Id == p.Id)
            }).ToList()
        };

        return View(electionViewModel);
    }

    // POST: Elections/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, ElectionViewModel electionViewModel)
    {
        // handle invalid model state
        if (!ModelState.IsValid) return View();

        // ensure id matches url
        if (id != electionViewModel.Id) return NotFound();

        // convert view model to model
        var election = new Election
        {
            Id = id,
            Name = electionViewModel.Name,
            StartDate = electionViewModel.StartDate,
            EndDate = electionViewModel.EndDate,
            Parties = electionViewModel.PartiesViewModel.Where(p => p.Selected).Select(p => new Party { Id = p.Id })
                .ToList()
        };

        try
        {
            // update election
            await _electionService.UpdateAsync(election);
            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Parties/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var election = await _electionService.GetAsync(id);
        return View(election);
    }

    // POST: Parties/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmedAsync(int id)
    {
        try
        {
            // delete election
            await _electionService.DeleteAsync(id);
            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}