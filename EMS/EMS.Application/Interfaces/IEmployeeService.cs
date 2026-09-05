using EMS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<PaginatedResult<EmployeeDto>> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<EmployeeDto> CreateAsync(EmployeeDto dto);
        Task<bool> UpdateAsync(Guid id, EmployeeDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
