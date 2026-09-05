import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';

  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}

  login(data: any) {
    return this.http.post<any>(`${this.apiUrl}/login`, data);
  }

  register(data: any) {
    return this.http.post<any>(`${this.apiUrl}/register`, data);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    const payload = JSON.parse(atob(token.split('.')[1]));
    return payload[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ];
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
