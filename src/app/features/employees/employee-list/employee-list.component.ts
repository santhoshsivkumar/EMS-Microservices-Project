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
      .getEmployees(this.page, 10, this.search)
      .subscribe((res) => {
        this.employees = res.data.items;
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
}
