using Web.Areas.Management.Models;

namespace Web.Areas.Management.Controllers;

[Area("Management")]
public class ElectionsController : Controller
{
    private readonly IElectionService _electionService;
    private readonly IPartiesService _partiesService;

    public ElectionsController(IElectionService electionService, IPartiesService partiesService)
    {
        _electionService = electionService;
        _partiesService = partiesService;
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
        var parties = await _partiesService.GetAllAsync();
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
        try
        {
            var electionModel = new Election
            {
                Name = electionViewModel.Name,
                StartDate = electionViewModel.StartDate,
                EndDate = electionViewModel.EndDate,
                Parties = electionViewModel.PartiesViewModel.Where(p => p.Selected).Select(p => new Party { Id = p.Id })
                    .ToList()
            };

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
        var election = await _electionService.GetAsync(id);
        var parties = await _partiesService.GetAllAsync();
        
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
        if (!ModelState.IsValid) return View();

        var election = new Election
        {
            Id = id,
            Name = electionViewModel.Name,
            StartDate = electionViewModel.StartDate,
            EndDate = electionViewModel.EndDate,
            Parties = electionViewModel.PartiesViewModel.Where(p => p.Selected).Select(p => new Party { Id = p.Id })
                .ToList()
        };

        await _electionService.UpdateAsync(election);

        try
        {
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