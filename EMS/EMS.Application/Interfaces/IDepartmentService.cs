using EMS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface IDepartmentService
    {

        Task<IEnumerable<DepartmentDto>> GetAllAsync();
            
        Task<DepartmentDto?> GetByIdAsync(Guid id);

        Task<DepartmentDto> CreateAsync(DepartmentDto dto);

        Task<bool> UpdateAsync(Guid id, DepartmentDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}
