import { NgOptimizedImage } from '@angular/common';
import { Component, Input, OnDestroy } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Subscription } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { CartService } from '../../cart/services/cart.service';
import { Cart } from '../../cart/models/cart.model';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [RouterLink, NgOptimizedImage],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css',
})
export class ProductCardComponent implements OnDestroy {
  @Input() id: string = '';
  @Input() title: string = '';
  @Input() description: string = '';
  @Input() imageUrl: string = '';
  @Input() price: string = '';

  private addCartSubscription?: Subscription;
  model?: Cart;

  constructor(
    private cardService: CartService,
    private cookieService: CookieService,
  ) {}

  onAddToCart(productId: string): void {
    let token = this.cookieService.get('Authorization');
    const decodedToken: any = jwtDecode(token);
    let appUserId: string =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid'
      ];

    this.model = {
      appUserId: appUserId,
      productId: productId,
    };
    this.addCartSubscription = this.cardService.addCart(this.model).subscribe({
      next: (res) => {},
    });
  }

  ngOnDestroy(): void {
    this.addCartSubscription?.unsubscribe();
  }
}
