using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fileImporter.Data;
using fileImporter.DTO;
using fileImporter.Interfaces;
using fileImporter.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace fileImporter.Repositories
{
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        private FileImporterContext _context { get; set; }

        public CustomerRepository(FileImporterContext context)
        {
            this._context = context;
        }

        public Task<List<Customer>> FindAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return this._context.Customers.ToListAsync();
        }

        public void RemoveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> SaveAsync(CustomerDTO customerDTO)
        {
            Customer customer = new Customer();

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Address = customerDTO.Address;
            customer.Phone = customerDTO.Phone;
            customer.City = customerDTO.City;

            this._context.Add(customer);
            await this._context.SaveChangesAsync();

            return customer;
        }

        public Task<Customer> UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveRangeAsync(List<Customer> customers)
        {
            this._context.AddRange(customers);
            await this._context.SaveChangesAsync();
        }
    }
}