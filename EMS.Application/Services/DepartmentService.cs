using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EMSDbContext _context;  

        public DepartmentService(EMSDbContext context) { _context = context; }


        public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            _context.Departments.Add(department);

            await _context.SaveChangesAsync();

            dto.Id = department.Id;

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if(department == null)
            {
                return false;
            }
            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
            }).ToListAsync();
        }

        public async Task<DepartmentDto?> GetByIdAsync(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);

            if(department == null)
            {
                return null;
            }
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };


        }

        public async Task<bool> UpdateAsync(Guid id, DepartmentDto dto)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return false;
            }
            department.Name = dto.Name;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
