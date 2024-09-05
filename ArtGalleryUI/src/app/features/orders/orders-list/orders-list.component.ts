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
  orders$?: Observable<OrderFull[]>;
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
          this.orders$ = this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize);
        }
      },
    });
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.orders$ = this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize);
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.orders$ = this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize);

  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.orders$ = this.orderService.getOrdersByUserId(this.userId, this.pageNumber, this.pageSize);

  }

  
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }
}
