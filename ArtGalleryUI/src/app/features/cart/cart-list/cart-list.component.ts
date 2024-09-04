import {
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { AsyncPipe, NgIf, NgOptimizedImage } from '@angular/common';
import { CartService } from '../services/cart.service';
import { CartResponse } from '../models/cart-response.model';
import { Product } from '../../products/models/product.model';
import { Payment } from '../models/payment.model';
import { AddPayment } from '../models/add-payment.model';
import { AddOrder } from '../../orders/models/add-order.model';
import { AddOrderItem } from '../../orders/models/add-orderItem.model';
import { OrderItem } from '../../orders/models/orderItem.model';
import { AddressListComponent } from '../../checkout/address-list/address-list.component';
import { AddressList } from '../../checkout/models/address-list.model';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [
    RouterLink,
    AsyncPipe,
    NgOptimizedImage,
    AddressListComponent,
    FormsModule,
    NgIf,
  ],
  templateUrl: './cart-list.component.html',
  styleUrl: './cart-list.component.css',
})
export class CartListComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  userId: any;
  productIds: string[] = [];
  cost: number = 0;
  totalCost: number = 0;
  shippingCost: number = 40;
  tax: number = 0;
  cartItems: any;
  paymentModel: AddPayment;
  orderModel: AddOrder;
  orderItemModel: AddOrderItem;
  orderItems: AddOrderItem[] = [];
  addressFlag: boolean = false;
  addressId?: string;
  isFormSubmitted: boolean = false;
  private paramsSubscription?: Subscription;
  private getCartsSubscription?: Subscription;
  private getProductsSubscription?: Subscription;
  private deleteCartItemSubscription?: Subscription;
  private deleteCartsSubscription?: Subscription;
  private deleteProductsSubscription?: Subscription;
  private createPaymentSubscription?: Subscription;
  private createOrderSubscription?: Subscription;
  private createOrderItemsSubscription?: Subscription;

  constructor(
    private cartService: CartService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    this.paymentModel = {
      amount: 0,
      paymentDate: new Date(),
      cardNumber: '',
      cardHolderName: '',
      expiryDate: new Date(),
    };
    this.orderModel = {
      appUserId: '',
      paymentId: '',
      addressId: '',
    };
    this.orderItemModel = {
      status: 'Order Placed',
      productCost: 0,
      shippingCost: 0,
      taxCost: 0,
      productId: '',
      orderId: '',
    };
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId = params.get('userId');
        if (this.userId) {
          this.getCartsSubscription = this.cartService
            .getCartsForUser(this.userId)
            .subscribe({
              next: (res) => {
                res.forEach((item) => this.productIds?.push(item.productId));
                this.cartItems = res;
                this.getProductsSubscription = this.cartService
                  .getCartProducts(this.productIds)
                  .subscribe({
                    next: (res) => {
                      this.products = res;
                      res.forEach((p) => (this.cost += p.price));
                      this.tax += this.cost / 10;
                      this.tax = Math.floor(this.tax * 100) / 100;
                      this.totalCost = this.cost + this.tax + this.shippingCost;
                    },
                  });
              },
            });
        }
      },
    });
  }

  onDeleteClick(cartItemId: string) {
    let cartId = '';
    for (let i = 0; i < this.cartItems.length; i++) {
      if (this.cartItems[i].productId == cartItemId) {
        cartId = this.cartItems[i].cartId;
      }
    }
    this.deleteCartItemSubscription = this.cartService
      .deleteCart(cartId)
      .subscribe({
        next: (response) => {
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate([`cart/${this.userId}`]);
            });
        },
      });
  }

  selectAddress(address: AddressList) {
    this.addressId = address.addressId;
    // this.onCheckout();
  }

  onCheckout() {
    if (this.addressFlag) {
      this.isFormSubmitted = true;
    }
    this.addressFlag = true;
    this.paymentModel.amount = this.totalCost;
    if (
      !this.paymentModel.amount ||
      !this.paymentModel.cardNumber ||
      !this.paymentModel.cardHolderName ||
      !this.paymentModel.expiryDate ||
      !this.paymentModel.paymentDate
    ) {
      return;
    }
    if (!this.addressId) {
      return;
    }
    const address = this.addressId;
    const userId = localStorage.getItem('user-id');
    this.createPaymentSubscription = this.cartService
      .createPayment(this.paymentModel)
      .subscribe({
        next: (res) => {
          this.orderModel = {
            paymentId: res.paymentId,
            addressId: address,
            appUserId: userId,
          };
          this.createOrderSubscription = this.cartService
            .createOrder(this.orderModel)
            .subscribe({
              next: (res) => {
                for (let i = 0; i < this.products.length; i++) {
                  this.orderItemModel = {
                    status: 'Order Placed',
                    productCost: this.products[i].price,
                    shippingCost: this.shippingCost,
                    taxCost:
                      Math.floor((this.products[i].price / 10) * 100) / 100,
                    orderId: res.orderId,
                    productId: this.products[i].productId,
                  };
                  this.orderItems.push(this.orderItemModel);
                }
                this.createOrderItemsSubscription = this.cartService
                  .createOrderItems(this.orderItems)
                  .subscribe({
                    next: (res) => {
                      this.deleteCartsSubscription = this.cartService
                        .deleteCarts(this.productIds)
                        .subscribe({
                          next: (res) => {
                            this.deleteProductsSubscription = this.cartService
                              .deleteProducts(this.productIds)
                              .subscribe({
                                next: (res) => {
                                  this.router.navigateByUrl(
                                    `/myorders/${userId}`,
                                  );
                                },
                              });
                          },
                        });
                    },
                  });
              },
            });
        },
      });
  }

  ngOnDestroy(): void {
    this.deleteCartItemSubscription?.unsubscribe();
    this.getCartsSubscription?.unsubscribe();
    this.getProductsSubscription?.unsubscribe();
    this.paramsSubscription?.unsubscribe();
    this.createOrderItemsSubscription?.unsubscribe();
    this.createOrderSubscription?.unsubscribe();
    this.createPaymentSubscription?.unsubscribe();
    this.deleteCartsSubscription?.unsubscribe();
    this.deleteProductsSubscription?.unsubscribe();
  }
}
