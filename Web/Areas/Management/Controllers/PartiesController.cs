namespace Web.Areas.Management.Controllers;

[Area("Management")]
public class PartiesController : Controller
{
    private readonly IPartiesService _partiesService;

    public PartiesController(IPartiesService partiesService)
    {
        _partiesService = partiesService;
    }

    // GET: Parties
    public async Task<ActionResult> Index()
    {
        var parties = await _partiesService.GetAllAsync();
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
            _partiesService.CreateAsync(party);
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
        var party = await _partiesService.GetAsync(id);
        return View(party);
    }

    // POST: Parties/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Party party)
    {
        if (id != party.Id) return NotFound();

        try
        {
            _partiesService.UpdateAsync(party);
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
        var party = await _partiesService.GetAsync(id);
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
            _partiesService.DeleteAsync(id);
            TempData["Success"] = true; // show success message
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}