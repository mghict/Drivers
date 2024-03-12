namespace Driver.Common.Models;

public class PersonDetailDto
{
    public Guid PersonCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string MobileNumber { get; set; }
    public string DisplayName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public bool IsActive { get; set; }

    public DocumentDetailDto? Document { get; set; }
}
