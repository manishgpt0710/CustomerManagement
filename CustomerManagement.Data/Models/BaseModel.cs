using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Data.Models
{
    public class BaseModel
    {
        [Key]
        public Int64 Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
    }
}
