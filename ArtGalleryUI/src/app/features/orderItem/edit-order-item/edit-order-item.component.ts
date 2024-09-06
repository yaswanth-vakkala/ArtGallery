import { Component, OnDestroy, OnInit } from '@angular/core';
import { EditOrderItem } from '../models/edit-orderItem.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderItemService } from '../services/order-item.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-order-item',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './edit-order-item.component.html',
  styleUrl: './edit-order-item.component.css'
})
export class EditOrderItemComponent implements OnInit, OnDestroy {
  model?:EditOrderItem;
  orderItemId: string | null = null;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private editOrderItemSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(private orderItemService: OrderItemService,
    private router: Router,
    private route: ActivatedRoute){
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.orderItemId = params.get('orderItemId');
        if (this.orderItemId) {
          this.orderItemService.getOrderItemByIdFull(this.orderItemId).subscribe({
            next: (response) => {
              this.model = response;
            },
          });
        }
      },
    });
  }

  onEditOrderItemSubmit(form:NgForm) {
    this.isFormSubmitted = true;
    if (this.orderItemId && this.model?.status && this.model.productCost && this.model.taxCost && 
      this.model.productCost && this.model.shippingCost && form.valid) {
      this.editOrderItemSubscription = this.orderItemService
        .editOrderItem(this.orderItemId, this.model)
        .subscribe({
          next: (response) => {
            this.successMessage = true;
            setTimeout(() => {
            this.successMessage = false;
            }, 5000);
            this.router.navigateByUrl('/admin/orders');
          },
          error: (res) => {
            this.errMessage = true;
            setTimeout(() => {
              this.errMessage = false;
            }, 5000);
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editOrderItemSubscription?.unsubscribe();
  }
}
