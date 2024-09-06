import { Component, OnInit } from '@angular/core';
import { OrderFull } from '../models/order-full.model';
import { Observable } from 'rxjs';
import { OrderService } from '../services/order.service';
import { AsyncPipe, DatePipe, NgOptimizedImage } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-all-orders',
  standalone: true,
  imports: [AsyncPipe, RouterLink, NgOptimizedImage, DatePipe, FormsModule],
  templateUrl: './all-orders.component.html',
  styleUrl: './all-orders.component.css',
})
export class AllOrdersComponent implements OnInit {
  orders$?: Observable<OrderFull[]>;
  orderCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.getOrdersCount().subscribe({
      next: (res) => {
        this.orderCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });

    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
    );
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    );
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    );
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    );
  }

  search(query: string) {
    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      query,
    );
    this.query = query;
    this.orderService.getOrdersCount(query).subscribe({
      next: (res) => {
        this.orderCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.orders$ = this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      sortBy,
      sortOrder,
    );
  }

  clearFilters() {
    window.location.reload();
    // this.router
    //   .navigateByUrl('/admin/categories', { skipLocationChange: true })
    //   .then(() => {
    //     this.router.navigate([`/`]);
    //   });
  }
}
