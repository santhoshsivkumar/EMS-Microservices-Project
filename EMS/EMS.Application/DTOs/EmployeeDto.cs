using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; }

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
