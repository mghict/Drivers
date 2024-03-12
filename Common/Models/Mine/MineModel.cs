namespace Driver.Common.Models;

public class MineModel
{
    public long Id { get; set; }
    public long MineCode { get; set; } = 0;
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationModel Location { get; set; }
    public ICollection<int>? MaterialIds {get;set; }
}
