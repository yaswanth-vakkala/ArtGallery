import { Component, OnDestroy, OnInit } from '@angular/core';
import { OrderItemFull } from '../models/orderItem-full.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { OrderItemService } from '../services/order-item.service';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-order-item-detail',
  standalone: true,
  imports: [RouterLink, NgOptimizedImage],
  templateUrl: './order-item-detail.component.html',
  styleUrl: './order-item-detail.component.css',
})
export class OrderItemDetailComponent implements OnInit, OnDestroy {
  model?: OrderItemFull;
  orderItemId: any;
  private getOrderItemFullSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private orderItemService: OrderItemService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (res) => {
        this.orderItemId = res.get('orderItemId');
        if (this.orderItemId) {
          this.getOrderItemFullSubscription = this.orderItemService
            .getOrderItemByIdFull(this.orderItemId)
            .subscribe({
              next: (res) => {
                this.model = res;
              },
            });
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.getOrderItemFullSubscription?.unsubscribe();
  }
}
