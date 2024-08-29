import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Cart } from '../models/cart.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { CartResponse } from '../models/cart-response.model';
import { Product } from '../../products/models/product.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  getCartsForUser(userId:string) : Observable<CartResponse[]>{
    return this.http.get<CartResponse[]>(`${environment.apiBaseUrl}/api/cart/${userId}?addAuth=true`);
  }

  addCart(model: Cart): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/cart?addAuth=true`,
      model,
    );
  }

  getCartProducts(productIds: string[]): Observable<Product[]>{
    return this.http.post<Product[]>(`${environment.apiBaseUrl}/api/product/cart`,{
      productIds
    });
  }

  deleteCart(cartItemId: string) : Observable<boolean>{
    return this.http.delete<boolean>(`${environment.apiBaseUrl}/api/cart/${cartItemId}`);
  }
}
