import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [AsyncPipe, RouterLink, ProductCardComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products$?: Observable<Product[]>;
  constructor(
    private productService: ProductService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.products$ = this.productService.getAllProducts();
  }

  sort(sortBy: string, sortOrder: string) {
    this.products$ = this.productService.getAllProducts(sortBy, sortOrder);
  }
}
