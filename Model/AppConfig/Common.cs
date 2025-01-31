using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AppConfig
{
    public class Common
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; } = "user";
        public string? UpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
