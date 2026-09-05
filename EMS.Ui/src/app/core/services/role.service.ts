import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private api = 'http://localhost:5000/api/roles';

  constructor(private http: HttpClient) {}

  getRoles(): Observable<any[]> {
    return this.http.get<any>(this.api).pipe(map((res: any) => res.data));
  }

  createRole(data: any): Observable<any> {
    return this.http.post(this.api, data);
  }

  updateRole(id: string, data: any): Observable<any> {
    return this.http.put(`${this.api}/${id}`, data);
  }

  deleteRole(id: string): Observable<any> {
    return this.http.delete(`${this.api}/${id}`);
  }
}
