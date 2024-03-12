namespace Driver.Common.Models;

public class PersonCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string MobileNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
}
