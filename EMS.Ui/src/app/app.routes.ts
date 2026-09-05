import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { authGuard } from './core/guards/auth.guard';
import { DepartmentListComponent } from './features/departments/department-list/department-list.component';
import { EmployeeListComponent } from './features/employees/employee-list/employee-list.component';
import { RoleListComponent } from './features/roles/role-list/role-list.component';
export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: 'departments',
    component: DepartmentListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'employees',
    component: EmployeeListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'roles',
    component: RoleListComponent,
    canActivate: [authGuard],
  },
];
