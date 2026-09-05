using EMS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();

        Task<RoleDto?> GetByIdAsync(Guid id);

        Task<RoleDto> CreateAsync(RoleDto dto);

        Task<bool> UpdateAsync(Guid id, RoleDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}
