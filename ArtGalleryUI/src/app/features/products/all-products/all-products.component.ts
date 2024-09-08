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
  styleUrl: './all-products.component.css',
})
export class AllProductsComponent implements OnInit, OnDestroy {
  products?: Product[];
  productCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 5;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';
  selectedProducts: Set<string> = new Set<string>();
  private deleteProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.productService.getProductCount().subscribe({
      next: (res) => {
        this.productCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
    this.productService
      .getAllProducts(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
  }

  toggleSelection(product: Product) {
    if (this.selectedProducts.has(product.productId)) {
      this.selectedProducts.delete(product.productId);
    } else {
      this.selectedProducts.add(product.productId);
    }
  }

  isSelected(product: Product): boolean {
    return this.selectedProducts.has(product.productId);
  }

  toggleSelectAll(event: any) {
    if (event.target.checked) {
      this.products?.forEach((product) =>
        this.selectedProducts.add(product.productId),
      );
    } else {
      this.selectedProducts.clear();
    }
  }

  areAllSelected(): boolean {
    return this.products?.length === this.selectedProducts.size;
  }

  deleteSelectedProducts() {
    if (confirm('Are you sure to delete the selected products?')) {
      const productIdsArray = Array.from(this.selectedProducts);
      this.productService.deleteProductsBulk(productIdsArray).subscribe({
        next: (res) => {
          this.productService
            .getAllProducts(
              this.query,
              this.sortBy,
              this.sortOrder,
              this.pageNumber,
              this.pageSize,
            )
            .subscribe({
              next: (res) => {
                this.products = res;
              },
            });
          this.selectedProducts.clear();
        },
      });
    }
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.productService
      .getAllProducts(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.productService
      .getAllProducts(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.productService
      .getAllProducts(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
  }

  search(query: string) {
    this.productService
      .getAllProducts(
        query,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
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
    this.productService
      .getAllProducts(
        this.query,
        sortBy,
        sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
  }

  clearFilters() {
    window.location.reload();
    // this.router
    //   .navigateByUrl('/admin/categories', { skipLocationChange: true })
    //   .then(() => {
    //     this.router.navigate([`/`]);
    //   });
  }

  onDeleteClick(id: string) {
    if (confirm('Are you to delete the product?')) {
      this.deleteProductSubscription = this.productService
        .deleteProduct(id)
        .subscribe({
          next: (response) => {
            this.router
              .navigateByUrl('/', { skipLocationChange: true })
              .then(() => {
                this.router.navigate(['admin/products']);
              });
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.deleteProductSubscription?.unsubscribe();
  }
}
