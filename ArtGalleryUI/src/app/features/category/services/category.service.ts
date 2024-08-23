import { HttpClient } from '@angular/common/http';
import { Injectable, model } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { environment } from '../../../../environments/environment.development';
import { AddCategory } from '../models/add-category.model';
import { EditCategory } from '../models/edit-category.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/category`);
  }

  getCategoryById(categoryId: string): Observable<Category> {
    return this.http.get<Category>(
      `${environment.apiBaseUrl}/api/category/${categoryId}`
    );
  }

  addCategory(model: AddCategory): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/category?addAuth=true`,
      model
    );
  }

  editCategory(categoryId: string, model: EditCategory): Observable<Category> {
    return this.http.put<Category>(
      `${environment.apiBaseUrl}/api/category/${categoryId}?addAuth=true`,
      model
    );
  }

  // editCategory(categoryId: string, model: EditCategory): Observable<Category> {
  //   return this.http.put<Category>(
  //     `${environment.apiBaseUrl}/api/category/${categoryId}`,
  //     model,
  //     {
  //       headers : {
  //         'Authorization' : this.cookieService.get('Authorization')
  //       }
  //     }
  //   );
  // }

  deleteCategory(categoryId: string): Observable<boolean> {
    return this.http.delete<boolean>(
      `${environment.apiBaseUrl}/api/category/${categoryId}?addAuth=true`
    );
  }
}
