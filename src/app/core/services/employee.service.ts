import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private api = 'http://localhost:5000/api/employees';

  constructor(private http: HttpClient) {}

  getEmployees(page = 1, size = 10, search = '') {
    return this.http.get<any>(
      `${this.api}?pageNumber=${page}&pageSize=${size}&search=${search}`,
    );
  }

  getEmployee(id: string) {
    return this.http.get(`${this.api}/${id}`);
  }

  createEmployee(data: any) {
    return this.http.post(this.api, data);
  }

  updateEmployee(id: string, data: any) {
    return this.http.put(`${this.api}/${id}`, data);
  }

  deleteEmployee(id: string) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
