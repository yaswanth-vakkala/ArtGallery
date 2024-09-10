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
  orders?: OrderFull[];
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

    this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.orderService.getAllOrders(
      this.pageNumber,
      this.pageSize,
      this.query,
      this.sortBy,
      this.sortOrder
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
  }

  search(query: string) {
    this.orderService.getAllOrders(
      1,
      this.pageSize,
      query,
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
    this.query = query;
    this.orderService.getOrdersCount(query).subscribe({
      next: (res) => {
        this.orderCount = res;
        this.pageNumber = 1;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.orderService.getAllOrders(
      1,
      this.pageSize,
      this.query,
      sortBy,
      sortOrder,
    ).subscribe({
      next: (res) => {
        this.orders =res;
      }
    });
    this.pageNumber = 1;
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
