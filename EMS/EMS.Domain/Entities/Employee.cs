using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entities
{
    public class Employee: BaseEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties - Foreign keys

        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Guid RoleId { get; set; } 
        public Role? Role { get; set; }

    }
}
