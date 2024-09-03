import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateProduct } from '../models/create-product.model';
import { Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './create-product.component.html',
  styleUrl: './create-product.component.css'
})
export class CreateProductComponent implements OnDestroy 
{
  model:CreateProduct;
  private createProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router,
  ){
    this.model={
      name:'',
      description:'',
      imageUrl:'',
      price:'',
      categoryId:'',
    };
  }

  onCreateProductSubmit(){
    this.createProductSubscription=this.productService
    .createProduct(this.model)
    .subscribe({
      next:(response)=>{
        this.router.navigateByUrl('/admin/products');
      },
    });
  }

  ngOnDestroy(): void {
    this.createProductSubscription?.unsubscribe();
  }

}
