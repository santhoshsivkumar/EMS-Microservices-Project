using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly EMSDbContext _context;

        public RoleService(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            return await _context.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();
        }

        public async Task<RoleDto?> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return null;
            return new RoleDto { Id = role.Id, Name = role.Name };
        }

        public async Task<RoleDto> CreateAsync(RoleDto dto)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            dto.Id = role.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(Guid id, RoleDto dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            role.Name = dto.Name;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
