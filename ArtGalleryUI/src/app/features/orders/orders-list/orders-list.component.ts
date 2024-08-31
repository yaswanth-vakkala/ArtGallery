import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { Order } from '../models/order.model';
import { OrderService } from '../services/order.service';
import { OrderFull } from '../models/order-full.model';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-orders-list',
  standalone: true,
  imports: [NgOptimizedImage, RouterLink],
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css',
})
export class OrdersListComponent implements OnInit {
  model?: OrderFull[];
  userId: any;
  private getOrdersSubscription?: Subscription;
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
          this.getOrdersSubscription = this.orderService
            .getOrdersByUserId(this.userId)
            .subscribe({
              next: (res) => {
                this.model = res;
              },
            });
        }
      },
    });
  }
}
