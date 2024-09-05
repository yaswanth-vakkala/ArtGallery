import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Order } from '../models/order.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { OrderFull } from '../models/order-full.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
  ) {}

  getOrdersByUserId(
    userId: string,
    pageNumber?: number,
    pageSize?: number,
  ): Observable<OrderFull[]> {
    let params = new HttpParams();
    if (pageNumber) {
      params = params.set('pageNumber', pageNumber);
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize);
    }

    return this.http.get<OrderFull[]>(
      `${environment.apiBaseUrl}/api/apporder/user/${userId}`,
      {
        params: params,
      },
    );
  }

  getAllOrders(
    pageNumber: number,
    pageSize: number,
    query?: string,
    sortBy?: string,
    sortOrder?: string,
  ): Observable<OrderFull[]> {
    let params = new HttpParams();
    if (pageNumber) {
      params = params.set('pageNumber', pageNumber);
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize);
    }
    if (query) {
      params = params.set('query', query);
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy);
    }

    if (sortOrder) {
      params = params.set('sortOrder', sortOrder);
    }

    return this.http.get<OrderFull[]>(
      `${environment.apiBaseUrl}/api/apporder`,
      {
        params: params,
      },
    );
  }

  getOrderCount(userId?: string): Observable<number> {
    return this.http.get<number>(
      `${environment.apiBaseUrl}/api/appOrder/count/${userId}`,
    );
  }

  getOrdersCount(query?: string): Observable<number> {
    let params = new HttpParams();

    if (query) {
      params = params.set('query', query);
    }
    return this.http.get<number>(
      `${environment.apiBaseUrl}/api/appOrder/ordersCount`,
      {
        params: params,
      },
    );
  }
}
