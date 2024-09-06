import { Component, OnDestroy } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AddCategory } from '../models/add-category.model';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Router, RouterLink } from '@angular/router';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule,RouterLink,NgIf],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css',
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategory;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private addCategorySubscription?: Subscription;

  constructor(
    private categoryService: CategoryService,
    private router: Router,
  ) {
    this.model = {
      name: '',
      description: '',
    };
  }

  onAddCategorySubmit(form:NgForm) {
    this.isFormSubmitted = true;
    if(this.model.name && form.valid){
      this.addCategorySubscription = this.categoryService
      .addCategory(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
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
    this.addCategorySubscription?.unsubscribe();
  }
}
