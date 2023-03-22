using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using fileImporter.Models;

namespace fileImporter.Data
{
    public class FileImporterContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            string server = "DEV\\MSSQLSERVER2";
            string database = "fileImporter";

            optionsBuilder
                .UseSqlServer($"Data Source={server};Initial Catalog={database};User ID=sa;Password=15974;TrustServerCertificate=True");
        }
    }
}