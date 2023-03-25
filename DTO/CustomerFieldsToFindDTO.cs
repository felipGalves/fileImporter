using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fileImporter.DTO
{
    public class CustomerFieldsToFindDTO
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public string? City { get; set; }
    }
}