import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/product.model';
import { NgOptimizedImage } from '@angular/common';

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

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
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

  ngOnDestroy(): void {
    this.getProductSubscription?.unsubscribe();
  }
}
