namespace Driver.Common.Models;

public record MineUpdateModel(long Id,string Name, LocationCreateModel Location, string Address,ICollection<int>? MaterialIds=null, long MineCode = 0);
