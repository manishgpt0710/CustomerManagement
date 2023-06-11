using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Data.Models;

public class Customer: BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public List<ContactInfo>? ContactInfos { get; set; }

    public List<Order>? Orders { get; set; }
}

