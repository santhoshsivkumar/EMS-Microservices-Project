import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../core/services/role.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-role-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './role-list.component.html',
})
export class RoleListComponent implements OnInit {
  roles: any[] = [];
  newRole = '';

  constructor(private roleService: RoleService) {}

  ngOnInit() {
    this.loadRoles();
  }

  loadRoles() {
    this.roleService.getRoles().subscribe((res) => {
      this.roles = res;
    });
  }

  createRole() {
    if (!this.newRole) return;

    this.roleService.createRole({ name: this.newRole }).subscribe(() => {
      this.newRole = '';
      this.loadRoles();
    });
  }

  deleteRole(id: string) {
    this.roleService.deleteRole(id).subscribe(() => {
      this.loadRoles();
    });
  }
}
