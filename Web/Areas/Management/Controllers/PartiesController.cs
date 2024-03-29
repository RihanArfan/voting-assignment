namespace Web.Areas.Management.Controllers;

[Area("Management")]
public class PartiesController : Controller
{
    private readonly IPartyService _partyService;

    public PartiesController(IPartyService partyService)
    {
        _partyService = partyService;
    }

    // GET: Parties
    public async Task<ActionResult> Index()
    {
        var parties = await _partyService.GetAllAsync();
        ViewBag.Success = Convert.ToBoolean(TempData["Success"] ?? false);
        return View(parties);
    }

    // GET: Parties/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Parties/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Party party)
    {
        try
        {
            // create party
            _partyService.CreateAsync(party);
            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Parties/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var party = await _partyService.GetAsync(id);
        return View(party);
    }

    // POST: Parties/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Party party)
    {
        // handle invalid model state
        if (!ModelState.IsValid) return View();

        // ensure id matches url
        if (id != party.Id) return NotFound();

        try
        {
            // update party
            _partyService.UpdateAsync(party);
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
        var party = await _partyService.GetAsync(id);
        return View(party);
    }

    // POST: Parties/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _partyService.DeleteAsync(id);
            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}