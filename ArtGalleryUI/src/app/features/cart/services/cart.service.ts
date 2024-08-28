import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Cart } from '../models/cart.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  addCart(model: Cart): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/cart?addAuth=true`,
      model,
    );
  }
}
