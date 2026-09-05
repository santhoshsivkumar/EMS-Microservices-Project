import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../../../core/services/department.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './department-list.component.html',
})
export class DepartmentListComponent implements OnInit {
  departments: any[] = [];
  newDepartment = '';

  constructor(private departmentService: DepartmentService) {}

  ngOnInit() {
    this.loadDepartments();
  }

  loadDepartments() {
    this.departmentService.getDepartments().subscribe((res) => {
      this.departments = res;
    });
  }

  createDepartment() {
    if (!this.newDepartment) return;

    this.departmentService
      .createDepartment({
        name: this.newDepartment,
      })
      .subscribe(() => {
        this.newDepartment = '';
        this.loadDepartments();
      });
  }

  deleteDepartment(id: string) {
    this.departmentService.deleteDepartment(id).subscribe(() => {
      this.loadDepartments();
    });
  }
}
