import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { SharedService } from '../../../common/services/shared.service';
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
    private sharedService: SharedService
  ) {
    
  }

  ngOnInit(): void {
    this.products$ = this.productService.getAllProducts();
    this.sharedService.message$.subscribe((query) => {
      this.search(query);
    });
  }

  search(query: string){
    this.products$ = this.productService.getAllProducts(query);
  }

  sort(sortBy: string, sortOrder: string) {
    this.products$ = this.productService.getAllProducts(sortBy, sortOrder);
  }
}
