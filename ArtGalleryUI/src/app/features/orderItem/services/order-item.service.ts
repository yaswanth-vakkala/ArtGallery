import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderItemFull } from '../models/orderItem-full.model';
import { environment } from '../../../../environments/environment.development';
import { EditOrderItem } from '../models/edit-orderItem.model';

@Injectable({
  providedIn: 'root',
})
export class OrderItemService {
  constructor(private http: HttpClient) {}

  getOrderItemByIdFull(orderItemId: string): Observable<OrderItemFull> {
    return this.http.get<OrderItemFull>(
      `${environment.apiBaseUrl}/api/orderItem/product/${orderItemId}?addAuth=true`,
    );
  }

  editOrderItem(orderItemId: string, orderItem:EditOrderItem): Observable<void>{
    return this.http.put<void>(`${environment.apiBaseUrl}/api/orderitem/${orderItemId}?addAuth=true`, orderItem);
  }
}
