import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getAllProducts(sortBy?:string, sortOrder?:string): Observable<Product[]> {
    let params = new HttpParams();
    if(sortBy){
      params = params.set('sortBy', sortBy);
    }

    if(sortOrder){
      params = params.set('sortOrder', sortOrder);
    }
    return this.http.get<Product[]>(`${environment.apiBaseUrl}/api/product`,{
      params: params
    });
  }

  getProductById(productId: string): Observable<Product> {
    return this.http.get<Product>(
      `${environment.apiBaseUrl}/api/product/${productId}`,
    );
  }
}
