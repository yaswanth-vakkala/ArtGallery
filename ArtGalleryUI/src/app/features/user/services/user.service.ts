import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AppUser } from '../models/appUser.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { AddUser } from '../models/add-user.model';
import { EditUser } from '../models/edit-user.model';
import { BulkAddResponse } from '../models/bulk-add-response';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  getAllUsers(
    query?: string,
    sortBy?: string,
    sortOrder?: string,
    pageNumber?: number,
    pageSize?: number,
  ): Observable<AppUser[]> {
    let params = new HttpParams();

    if (query) {
      params = params.set('query', query);
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy);
    }

    if (sortOrder) {
      params = params.set('sortOrder', sortOrder);
    }

    if (pageNumber) {
      params = params.set('pageNumber', pageNumber);
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize);
    }

    return this.http.get<AppUser[]>(
      `${environment.apiBaseUrl}/api/appUser?addAuth=true`,
      {
        params: params,
      },
    );
  }

  getUserCount(query?: string): Observable<number> {
    let params = new HttpParams();

    if (query) {
      params = params.set('query', query);
    }
    return this.http.get<number>(
      `${environment.apiBaseUrl}/api/appuser/count?addAuth=true`,
      {
        params: params,
      },
    );
  }

  getUserById(userId: string): Observable<AppUser> {
    return this.http.get<AppUser>(
      `${environment.apiBaseUrl}/api/appUser/${userId}?addAuth=true`,
    );
  }

  addUser(user: AddUser): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/auth/admin/register?addAuth=true`,
      user,
    );
  }

  addUsersBulk(selectedFile: File): Observable<BulkAddResponse[]> {
    const formData = new FormData();
    formData.append('file', selectedFile, selectedFile.name);
    return this.http.post<BulkAddResponse[]>(
      `${environment.apiBaseUrl}/api/auth/admin/register/bulk?addAuth=true`,
      formData,
    );
  }

  editUser(userId: string, user: EditUser): Observable<AppUser> {
    return this.http.put<AppUser>(
      `${environment.apiBaseUrl}/api/appUser/${userId}?addAuth=true`,
      user,
    );
  }

  deleteUser(userId: string): Observable<boolean> {
    return this.http.delete<boolean>(
      `${environment.apiBaseUrl}/api/appUser/${userId}?addAuth=true`,
    );
  }

  deleteUsersBulk(userids: string[]): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/appUser/delete/bulk?addAuth=true`,
      userids,
    );
  }
}
