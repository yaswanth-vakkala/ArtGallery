import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../models/login-request.model';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../../../environments/environment.development';
import { RegisterRequest } from '../models/register-request.model';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiBaseUrl}/api/auth/login`,
      {
        email: request.email,
        password: request.password,
      },
    );
  }

  register(request: RegisterRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/auth/register`, {
      email: request.email,
      password: request.password,
      firstName: request.firstName,
      lastName: request.lastName,
    });
  }

  setUser(user: User): void {
   // debugger

    let token = this.cookieService.get('Authorization');
    const decodedToken: any = jwtDecode(token);
    const claims: any =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];
    this.$user.next(user);
    // localStorage.setItem(
    //   'user-id',
    //   decodedToken[
    //     'http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid'
    //   ],
    // );
    // localStorage.setItem(
    //   'user-email',
    //   decodedToken[
    //     'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
    //   ],
    // );
    // localStorage.setItem(
    //   'user-roles',
    //   Array.isArray(claims) ? claims.join(',') : claims,
    // );
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    //debugger
    let token=this.cookieService.get('Authorization');
    const decodedToken: any=jwtDecode(token);
    let id: string =
    decodedToken[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid'
    ];
    let email: string =
    decodedToken[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
    ];
    let roles: string[] =
    decodedToken[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ];
    //const email = localStorage.getItem('user-email');
    //const id = localStorage.getItem('user-id');
    //const roles = localStorage.getItem('user-roles');

    if (id && email && roles) {
      //debugger
      const user: User = {
        id: id,
        email: email,
        roles: roles,
      };
      //console.log(roles)

      return user;
    }

    return undefined;
  }

  logout(): void {
    //localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
