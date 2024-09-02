import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TopSellingProductDto } from '../models/TopSellingProductDto';
import { SalesOverTimeDto } from '../models/SalesOverTimeDto';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private apiUrl = 'https://localhost:7087/api/statistics'; // Update the base URL as needed

  constructor(private http: HttpClient) { }

  // Get total sales count
  getTotalSales(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-sales`);
  }

  // Get category order counts
  getCategoryOrderCounts(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/category-order-counts`);
  }

  // Get monthly sales by year
  getMonthlySales(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/monthly-sales`);
  }

  // Get total products sold
  getTotalProductsSold(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/total-products-sold`);
  }

  // Get top selling products
  getTopSellingProducts(topN: number): Observable<TopSellingProductDto[]> {
    const params = new HttpParams().set('topN', topN.toString());
    return this.http.get<TopSellingProductDto[]>(`${this.apiUrl}/top-selling-products`, { params });
  }

  // Get orders in a specified date range
  getOrdersInDateRange(startDate: Date, endDate: Date): Observable<SalesOverTimeDto[]> {
    const params = new HttpParams()
      .set('startDate', startDate.toISOString())
      .set('endDate', endDate.toISOString());
    return this.http.get<SalesOverTimeDto[]>(`${this.apiUrl}/orders-in-date-range`, { params });
  }
}
