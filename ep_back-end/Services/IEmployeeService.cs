using ep_back_end.Entities;
using ep_back_end.Models;

namespace ep_back_end.Services
{
    public interface IEmployeeService
    {
        public Task<PagedResult<Employee>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Employee?> GetByIdAsync(int id);
        public Task<IEnumerable<Employee>> GetByNameAsync(string name);
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}
