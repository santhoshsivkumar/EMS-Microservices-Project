using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property - Collection of Employees

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
