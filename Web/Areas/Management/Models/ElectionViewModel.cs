namespace Web.Areas.Management.Models;

public class ElectionViewModel : Election
{
    public List<ElectionPartyViewModel> PartiesViewModel { get; set; } = new();
}