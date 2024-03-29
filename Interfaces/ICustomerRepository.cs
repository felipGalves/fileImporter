using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fileImporter.DTO;

namespace fileImporter.Interfaces
{
    public interface ICustomerRepository<Customer>
    {
        Task<List<Customer>> GetAllAsync();
        Task<List<Customer>> GetAllAsync(CustomerFieldsToFindDTO customerFieldsToFindDTO);
        Task<List<Customer>> FindAsync();
        Task<Customer> UpdateAsync();
        Task<Customer> SaveAsync(CustomerDTO customer); 
        Task SaveRangeAsync(List<Customer> customers); 
        Task RemoveAsync(CustomerFilterDTO customerFilterDTO); 
        Task RemoveAllAsync(); 
    }
}