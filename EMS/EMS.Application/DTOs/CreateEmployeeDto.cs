using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, 10000000)]
        public decimal Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties - Foreign keys

        public Guid DepartmentId { get; set; }

        public Guid RoleId { get; set; }    
    }
}
