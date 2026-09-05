using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EMSDbContext _context;

        public EmployeeService(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Employees.Include(e => e.Department).Include(e => e.Role).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.FirstName.Contains(search) ||
                    e.LastName.Contains(search) ||
                    e.Email.Contains(search));
            }
            var totalCount = await query.CountAsync();

            var employees = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Salary = e.Salary,
                    DateOfBirth = e.DateOfBirth,
                    JoiningDate = e.JoiningDate,
                    IsActive = e.IsActive,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department!.Name,
                    RoleId = e.RoleId,
                    RoleName = e.Role!.Name
                }).ToListAsync();

            return new PaginatedResult<EmployeeDto>
            {
                Items = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _context.Employees.Include(e => e.Department).Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return null;
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                JoiningDate = employee.JoiningDate,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department!.Name,
                RoleId = employee.RoleId,
                RoleName = employee.Role!.Name
            };
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
        {
            var employee = new Domain.Entities.Employee
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Salary = dto.Salary,
                DateOfBirth = dto.DateOfBirth,
                JoiningDate = dto.JoiningDate,
                IsActive = dto.IsActive,
                DepartmentId = dto.DepartmentId,
                RoleId = dto.RoleId
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(employee.Id) ?? throw new Exception("Error creating employee");
        }

        public async Task<bool> UpdateAsync(Guid id, EmployeeDto dto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.Salary = dto.Salary;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.JoiningDate = dto.JoiningDate;
            employee.IsActive = dto.IsActive;
            employee.DepartmentId = dto.DepartmentId;
            employee.RoleId = dto.RoleId;

            _context.Employees.Update(employee);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
