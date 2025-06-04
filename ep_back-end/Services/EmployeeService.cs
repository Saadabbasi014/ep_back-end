using ep_back_end.Entities;
using ep_back_end.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ep_back_end.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Employee>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Employees.CountAsync();
            var data = await _context.Employees
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Employee>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Records = data
            };
        }


        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees
                .Where(e => e.Name.Contains(name))
                .ToListAsync();
        }


        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var exists = await _context.Employees.AnyAsync(e => e.Id == employee.Id);
            if (!exists) return false;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
