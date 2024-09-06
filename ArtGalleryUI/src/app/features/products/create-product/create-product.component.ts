import { Component, OnDestroy } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CreateProduct } from '../models/create-product.model';
import { Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { Router, RouterLink } from '@angular/router';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [FormsModule, NgOptimizedImage, RouterLink,NgIf],
  templateUrl: './create-product.component.html',
  styleUrl: './create-product.component.css'
})
export class CreateProductComponent implements OnDestroy 
{
  model:CreateProduct;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private createProductSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router,
  ){
    this.model={
      name:'',
      description:'',
      imageUrl:'',
      price:0,
      categoryId:'',
    };
  }

  onCreateProductSubmit(form:NgForm){
    this.isFormSubmitted=true;
    if(this.model.name && this.model.description && this.model.imageUrl && this.model.price && this.model.categoryId && form.valid){
      this.createProductSubscription=this.productService
    .createProduct(this.model)
    .subscribe({
      next:(response)=>{
        this.router.navigateByUrl('/admin/products');
        this.successMessage = true;
          setTimeout(() => {
          this.successMessage = false;
          }, 5000);
      },
      error: (res) => {
        this.errMessage = true;
        setTimeout(() => {
          this.errMessage = false;
        }, 5000);
      }
    });
    }
    
  }

  ngOnDestroy(): void {
    this.createProductSubscription?.unsubscribe();
  }

}
