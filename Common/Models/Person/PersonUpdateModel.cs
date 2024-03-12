namespace Driver.Common.Models;

public class PersonUpdateModel
{
    public Guid PersonCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string MobileNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }

}
