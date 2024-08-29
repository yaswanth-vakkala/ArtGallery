import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { AsyncPipe, NgOptimizedImage } from '@angular/common';
import { CartService } from '../services/cart.service';
import { CartResponse } from '../models/cart-response.model';
import { Product } from '../../products/models/product.model';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe, NgOptimizedImage],
  templateUrl: './cart-list.component.html',
  styleUrl: './cart-list.component.css'
})
export class CartListComponent implements OnInit {
  products: Product[] = [];
  userId: any;
  productIds: string[]= [];
  cost:number= 0;
  totalCost:number= 0;
  shippingCost:number= 40;
  tax:number= 0;
  cartItems:any;
  private paramsSubscription?: Subscription;
  private getCartsSubscription? : Subscription;
  private getProductsSubscription? : Subscription;
  private deleteCartItemSubscription?: Subscription;

  constructor(private cartService: CartService, private router: Router
    ,private route:ActivatedRoute){
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId = params.get('userId');
        if(this.userId){
          this.getCartsSubscription = this.cartService.getCartsForUser(this.userId).subscribe({
            next:(res) => {
              res.forEach(item => this.productIds?.push(item.productId));
              this.cartItems = res;
              this.getProductsSubscription = this.cartService.getCartProducts(this.productIds).subscribe({
                next: (res)=>{
                  this.products = res;
                  res.forEach(p => this.cost += p.price);
                  this.tax += this.cost / 10;
                  this.tax = Math.floor(this.tax*100)/100;
                  this.totalCost = this.cost + this.tax + this.shippingCost;
                }
              });
            }
          });
        }
      }
    });
  }

  onDeleteClick(cartItemId: string) {
    let cartId = '';
    for(let i=0; i<this.cartItems.length; i++){
      if(this.cartItems[i].productId == cartItemId){
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
}
