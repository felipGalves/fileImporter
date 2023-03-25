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
using LinqKit;
using fileImporter.Utils;

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

        public async Task<List<Customer>> GetAllAsync()
        {
            return await this._context.Customers.ToListAsync();
        }

        public async Task<List<Customer>> GetAllAsync(CustomerFieldsToFindDTO customerFieldsToFindDTO)
        {
            var predicate = PredicateBuilder.New<Customer>();

            if (Converters.ToInt32(customerFieldsToFindDTO?.ID) != 0)
                predicate = predicate
                    .And(x 
                        => EF.Functions.Like(x.ID.ToString(), 
                        customerFieldsToFindDTO.ID.ToString()
                    ));

            if (!string.IsNullOrWhiteSpace(customerFieldsToFindDTO.Name)) 
                predicate = predicate.And(x => EF.Functions.Like(x.Name, customerFieldsToFindDTO.Name));

            if (!string.IsNullOrWhiteSpace(customerFieldsToFindDTO.Address))
                predicate = predicate.And(x => EF.Functions.Like(x.Address, customerFieldsToFindDTO.Address));

            if (!string.IsNullOrWhiteSpace(customerFieldsToFindDTO.Email))
                predicate = predicate.And(x => EF.Functions.Like(x.Email, customerFieldsToFindDTO.Email));

            if (Converters.ToInt32(customerFieldsToFindDTO?.Phone) != 0)
                predicate = predicate
                    .And(x 
                        => EF.Functions.Like(x.Phone.ToString(), 
                        customerFieldsToFindDTO.Phone.ToString()
                    ));
            
            if (!string.IsNullOrWhiteSpace(customerFieldsToFindDTO.City))
                predicate = predicate.And(x => EF.Functions.Like(x.City, customerFieldsToFindDTO.City));
            
            return await this._context.Customers
                .Where(predicate)
                .ToListAsync();
        }

        public async Task RemoveAsync(CustomerFilterDTO customerFilterDTO)
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

        public async Task RemoveAllAsync()
        {
            this._context.RemoveRange(await this.GetAllAsync());
            this._context.SaveChanges();
        }
    }
}