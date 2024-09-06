import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { EditProduct } from '../models/edit-product.model';
import { Subscription } from 'rxjs';
import { ProductService } from '../services/product.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports: [FormsModule, NgOptimizedImage, RouterLink,NgIf],
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.css'
})
export class EditProductComponent implements OnInit, OnDestroy{
  productId: string | null=null;
  model?: EditProduct;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private editProductSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.paramsSubscription=this.route.paramMap.subscribe({
      next:(params)=>{
        this.productId=params.get('productId');
        if(this.productId){
          this.productService.getProductById(this.productId).subscribe({
            next: (response)=>{
              this.model={
                name:response.name,
                description: response.description,
                imageUrl: response.imageUrl,
                price: response.price,
                status: response.status,
                categoryId: response.category.categoryId,
              }
              // console.log(this.model);
            },
          });
        }
      }
    })
  }

  onEditProductSubmit(form:NgForm){
    this.isFormSubmitted=true;
    if(this.productId && this.model?.name && this.model?.description && this.model?.imageUrl && this.model?.price && this.model?.status && this.model?.categoryId && form.valid){
      this.editProductSubscription=this.productService
      .editProduct(this.productId, this.model)
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
    this.paramsSubscription?.unsubscribe();
    this.editProductSubscription?.unsubscribe();
  }

}
