import { AsyncPipe, DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { FormsModule } from '@angular/forms';
import { BulkProductsResponse } from '../models/bulk-products-response';

@Component({
  selector: 'app-all-products',
  standalone: true,
  imports: [RouterLink, AsyncPipe, FormsModule, DatePipe],
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
  selectedFile?: File;
  errMessage: boolean = false;
  selectedProducts: Set<string> = new Set<string>();
  addBulkProductsResponse?: BulkProductsResponse[];
  private deleteProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.productService.getProductCountForAdmin().subscribe({
      next: (res) => {
        this.productCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
    this.productService
      .getAllProductsForAdmin(
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
            .getAllProductsForAdmin(
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
      .getAllProductsForAdmin(
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
      .getAllProductsForAdmin(
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
      .getAllProductsForAdmin(
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
      .getAllProductsForAdmin(query, undefined, undefined, 1, this.pageSize)
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
    this.query = query;
    this.productService.getProductCountForAdmin(query).subscribe({
      next: (res) => {
        this.productCount = res;
        this.pageNumber = 1;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.productService
      .getAllProductsForAdmin(this.query, sortBy, sortOrder, 1, this.pageSize)
      .subscribe({
        next: (res) => {
          this.products = res;
        },
      });
    this.pageNumber = 1;
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

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadFile() {
    const maxFileSize = 4 * 1024 * 1024;
    if (this.selectedFile) {
      const validTypes = [
        'application/vnd.ms-excel',
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      ];

      if (!validTypes.includes(this.selectedFile.type)) {
        alert('Please upload a valid Excel file (.xls or .xlsx).');
        return;
      }

      if (this.selectedFile.size > maxFileSize) {
        alert('File size exceeds 4MB. Please upload a smaller file.');
        return;
      }
      this.productService.addProductsBulk(this.selectedFile).subscribe({
        next: (res) => {
          this.addBulkProductsResponse = res;
        },
        error: (response) => {
          this.errMessage = true;
          setTimeout(() => {
            this.errMessage = false;
          }, 5000);
        },
      });
    } else {
      alert('No file selected!');
    }
  }

  getSuccessCount(): number {
    return this.addBulkProductsResponse!.filter(
      (product) => product.status === 'success',
    ).length;
  }

  getFailedCount(): number {
    return this.addBulkProductsResponse!.filter(
      (product) => product.status === 'failed',
    ).length;
  }

  refreshPage() {
    window.location.reload();
  }

  ngOnDestroy(): void {
    this.deleteProductSubscription?.unsubscribe();
  }
}
