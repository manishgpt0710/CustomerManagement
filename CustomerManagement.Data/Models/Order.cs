using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Data.Models;

public class Order: BaseModel
{
    public long CustomerId { get; set; }
    public string ProductName { get; set; }
    public decimal Rate { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}

