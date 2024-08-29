import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/product.model';
import { NgOptimizedImage } from '@angular/common';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { Cart } from '../../cart/models/cart.model';
import { CartService } from '../../cart/services/cart.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [NgOptimizedImage],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css',
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  productId: string | null = null;
  model?: Product;
  private getProductSubscription?: Subscription;
  private addCartSubscription?: Subscription;
  cartModel?: Cart;
  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private cookieService: CookieService,
    private cartService: CartService,
  ) {}

  ngOnInit(): void {
    this.getProductSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.productId = params.get('productId');
        if (this.productId) {
          this.productService.getProductById(this.productId).subscribe({
            next: (res) => {
              this.model = res;
            },
          });
        }
      },
    });
  }

  onAddToCart(productId: string): void {
    let token = this.cookieService.get('Authorization');
    const decodedToken: any = jwtDecode(token);
    let appUserId: string =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid'
      ];

    this.cartModel = {
      appUserId: appUserId,
      productId: productId,
    };
    this.addCartSubscription = this.cartService.addCart(this.cartModel).subscribe({
      next: (res) => {},
    });
  }

  ngOnDestroy(): void {
    this.getProductSubscription?.unsubscribe();
  }
}
