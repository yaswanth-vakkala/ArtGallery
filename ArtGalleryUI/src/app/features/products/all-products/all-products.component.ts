import { AsyncPipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-all-products',
  standalone: true,
  imports: [RouterLink, AsyncPipe, FormsModule],
  templateUrl: './all-products.component.html',
  styleUrl: './all-products.component.css'
})
export class AllProductsComponent implements OnInit, OnDestroy{
  products$?: Observable<Product[]>;
  productCount : number = 0;
  pageNumber: number = 1;
  pageSize: number = 5;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';
  private deleteProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getProductCount().subscribe({
      next: (res) => {
        this.productCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      }
    });
    this.products$ = this.productService.getAllProducts( undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize,);
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.products$ = this.productService.getAllProducts(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.products$ = this.productService.getAllProducts(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.products$ = this.productService.getAllProducts(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  search(query: string) {
    this.products$ = this.productService.getAllProducts(
      query,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize,
    );
    this.query = query;
    this.productService.getProductCount(query).subscribe({
      next: (res) => {
        this.productCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.products$ = this.productService.getAllProducts(
      this.query,
      sortBy,
      sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  clearFilters() {
    window.location.reload();
    // this.router
    //   .navigateByUrl('/admin/categories', { skipLocationChange: true })
    //   .then(() => {
    //     this.router.navigate([`/`]);
    //   });
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
