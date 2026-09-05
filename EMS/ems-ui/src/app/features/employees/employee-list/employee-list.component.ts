import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../core/services/employee.service';
import { DepartmentService } from '../../../core/services/department.service';
import { RoleService } from '../../../core/services/role.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-list.component.html',
})
export class EmployeeListComponent implements OnInit {
  employees: any[] = [];
  departments: any[] = [];
  roles: any[] = [];
  page = 1;
  totalPages = 1;
  pageSize = 10;
  search = '';

  newEmployee: any = {
    firstName: '',
    lastName: '',
    email: '',
    salary: 0,
    departmentId: '',
    roleId: 'User',
    dateOfBirth: '1995-01-01',
    joiningDate: new Date().toISOString().split('T')[0],
    isActive: true,
  };
  editingEmployeeId: string | null = null;

  editEmployee: any = {
    firstName: '',
    lastName: '',
    email: '',
    salary: 0,
    departmentId: '',
    roleId: '',
    dateOfBirth: '',
    joiningDate: '',
    isActive: true,
  };
  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private roleService: RoleService,
  ) {}
  ngOnInit() {
    this.loadEmployees();
    this.loadDepartmentsAndRoles();
  }

  loadEmployees() {
    this.employeeService
      .getEmployees(this.page, this.pageSize, this.search)
      .subscribe((res) => {
        const result = res.data;
        console.log('Employee List:', result);

        this.employees = result.items;
        this.totalPages = result.totalPages;
        this.page = result.pageNumber;
      });
  }

  loadDepartmentsAndRoles() {
    this.departmentService.getDepartments().subscribe((res) => {
      this.departments = res;
    });
    this.roleService.getRoles().subscribe((res) => {
      this.roles = res;
    });
  }

  createEmployee() {
    this.employeeService.createEmployee(this.newEmployee).subscribe({
      next: () => {
        this.loadEmployees();
      },
      error: (err) => {
        console.error('Error creating employee:', err.error);
      },
    });
  }

  deleteEmployee(id: string) {
    this.employeeService
      .deleteEmployee(id)
      .subscribe(() => this.loadEmployees());
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.page++;
      this.loadEmployees();
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.loadEmployees();
    }
  }

  startEdit(emp: any) {
    this.editingEmployeeId = emp.id;

    this.editEmployee = {
      firstName: emp.firstName,
      lastName: emp.lastName,
      email: emp.email,
      salary: emp.salary,
      departmentId: emp.departmentId,
      roleId: emp.roleId,
      dateOfBirth: emp.dateOfBirth?.split('T')[0],
      joiningDate: emp.joiningDate?.split('T')[0],
      isActive: emp.isActive,
    };
  }
  cancelEdit() {
    this.editingEmployeeId = null;
  }

  updateEmployee() {
    if (!this.editingEmployeeId) return;

    this.employeeService
      .updateEmployee(this.editingEmployeeId, this.editEmployee)
      .subscribe(() => {
        this.editingEmployeeId = null;

        this.loadEmployees();
      });
  }
}
