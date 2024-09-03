import { AsyncPipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-all-products',
  standalone: true,
  imports: [RouterLink, AsyncPipe],
  templateUrl: './all-products.component.html',
  styleUrl: './all-products.component.css'
})
export class AllProductsComponent implements OnInit, OnDestroy{
  products$?: Observable<Product[]>;
  private deleteProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.products$=this.productService.getAllProducts();
  }

  onDeleteClick(id: string){
    this.deleteProductSubscription=this.productService
      .deleteProduct(id)
      .subscribe({
        next:(response)=>{
          this.router
          .navigateByUrl('/',{skipLocationChange:true})
          .then(() => {
            this.router.navigate(['admin/products']);
          });
        },
      });

  }

  ngOnDestroy(): void {
    this.deleteProductSubscription?.unsubscribe();
  }
}
