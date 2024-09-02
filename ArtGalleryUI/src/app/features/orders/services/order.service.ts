import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Order } from '../models/order.model';
import {Observable} from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { OrderFull } from '../models/order-full.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http:HttpClient, private cookieService: CookieService) { }

  getOrdersByUserId(userId:string): Observable<OrderFull[]>{
    return this.http.get<OrderFull[]>(`${environment.apiBaseUrl}/api/apporder/user/${userId}`);
  }
}
