import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { Order } from '../models/order.model';
import { OrderService } from '../services/order.service';
import { OrderFull } from '../models/order-full.model';
import { AsyncPipe, DatePipe, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-orders-list',
  standalone: true,
  imports: [NgOptimizedImage, RouterLink, AsyncPipe, DatePipe],
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css',
})
export class OrdersListComponent implements OnInit, OnDestroy {
  orders?: OrderFull[];
  orderCount : number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  userId: any;
  private paramsSubscription?: Subscription;

  constructor(
    private orderService: OrderService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (res) => {
        this.userId = res.get('userId');
        if (this.userId) {
          this.orderService.getOrderCount(this.userId).subscribe({
            next: (res) => {
              this.orderCount = res;
              this.paginationList = new Array(Math.ceil(res / this.pageSize));
            }
          });
          this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize)
          .subscribe({
            next: (res) => {
              this.orders =res;
            }
          });
        }
      },
    });
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize)
    .subscribe({
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
    this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize)
    .subscribe({
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
    this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize)
    .subscribe({
      next: (res) => {
        this.orders =res;
      }
    });

  }

  
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }
}
