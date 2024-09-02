import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { SharedService } from '../../../common/services/shared.service';
import { ProductCardComponent } from '../product-card/product-card.component';
// import { SearchComponent } from "../../search/search.component";

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [AsyncPipe, RouterLink, ProductCardComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products$?: Observable<Product[]>;
  productsCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  query:string='';
  constructor(
    private productService: ProductService,
    private router: Router,
    private sharedService: SharedService
  ) {
    
  }

  // searchText:string='';

  // onSearchTextEntered(searchValue: string){
  //   this.searchText=searchValue;
  //   // console.log(this.searchText);
  // }

  ngOnInit(): void {
    this.productService.getProductCount().subscribe({
      next: (res) => {
        this.productsCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      }
    });
    this.products$ = this.productService.getAllProducts(undefined,undefined,undefined,this.pageNumber, this.pageSize);
    this.sharedService.message$.subscribe((query) => {
      this.query = query;
      this.search(query);
    });
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.products$ = this.productService.getAllProducts(undefined,undefined,undefined,this.pageNumber, this.pageSize);
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.products$ = this.productService.getAllProducts(undefined,undefined,undefined,this.pageNumber, this.pageSize);
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.products$ = this.productService.getAllProducts(undefined,undefined,undefined,this.pageNumber, this.pageSize);
  }

  search(query: string){
    this.products$ = this.productService.getAllProducts(query, undefined, undefined, this.pageNumber, this.pageSize);
    this.productService.getProductCount(query).subscribe({
      next: (res) => {
        this.productsCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      }
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.products$ = this.productService.getAllProducts(this.query, sortBy, sortOrder, this.pageNumber, this.pageSize);
  }
}
