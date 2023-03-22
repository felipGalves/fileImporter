using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fileImporter.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string City { get; set; }
    }

    internal class CustomerMap : EntityTypeBuilder<Customer>
    {
        public CustomerMap(IMutableEntityType entityType) : base(entityType)
        {
        }

        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.HasKey(x => x.ID);
        }
    }
}