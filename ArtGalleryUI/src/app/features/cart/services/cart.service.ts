import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Cart } from '../models/cart.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { CartResponse } from '../models/cart-response.model';
import { Product } from '../../products/models/product.model';
import { Payment } from '../models/payment.model';
import { AddPayment } from '../models/add-payment.model';
import { AddOrder } from '../models/add-order.model';
import { Order } from '../models/order.model';
import { AddOrderItem } from '../models/add-orderItem.model';
import { OrderItem } from '../models/orderItem.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  getCartsForUser(userId: string): Observable<CartResponse[]> {
    return this.http.get<CartResponse[]>(
      `${environment.apiBaseUrl}/api/cart/${userId}?addAuth=true`,
    );
  }

  addCart(model: Cart): Observable<void> {
    return this.http.post<void>(
      `${environment.apiBaseUrl}/api/cart?addAuth=true`,
      model,
    );
  }

  createPayment(paymentModel: AddPayment): Observable<Payment> {
    return this.http.post<Payment>(
      `${environment.apiBaseUrl}/api/payment`,
      paymentModel,
    );
  }

  createOrder(orderModel: AddOrder): Observable<Order> {
    return this.http.post<Order>(
      `${environment.apiBaseUrl}/api/apporder`,
      orderModel,
    );
  }

  createOrderItems(orderItemModel: AddOrderItem[]): Observable<OrderItem[]> {
    return this.http.post<OrderItem[]>(
      `${environment.apiBaseUrl}/api/orderitem/addMultiple`,
      orderItemModel,
    );
  }

  getCartProducts(productIds: string[]): Observable<Product[]> {
    return this.http.post<Product[]>(
      `${environment.apiBaseUrl}/api/product/cart`,
      {
        productIds,
      },
    );
  }

  deleteProducts(productIds: string[]): Observable<boolean> {
    return this.http.post<boolean>(
      `${environment.apiBaseUrl}/api/product/deleteproducts`,
      productIds,
    );
  }

  deleteCart(cartItemId: string): Observable<boolean> {
    return this.http.delete<boolean>(
      `${environment.apiBaseUrl}/api/cart/${cartItemId}`,
    );
  }

  deleteCarts(productIds: string[]): Observable<boolean> {
    return this.http.post<boolean>(
      `${environment.apiBaseUrl}/api/cart/deleteCarts`,
      productIds,
    );
  }
}
