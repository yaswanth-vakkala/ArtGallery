import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { EditCategory } from '../models/edit-category.model';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-category',
  standalone: true,
  imports: [FormsModule,RouterLink,NgIf],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css',
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  categoryId: string | null = null;
  model?: EditCategory;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private editCategorySubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.categoryId = params.get('categoryId');
        if (this.categoryId) {
          this.categoryService.getCategoryById(this.categoryId).subscribe({
            next: (response) => {
              this.model = response;
            },
          });
        }
      },
    });
  }

  onEditCategorySubmit(form:NgForm) {
    this.isFormSubmitted = true;
    if (this.categoryId && this.model?.name  && form.valid) {
      this.editCategorySubscription = this.categoryService
        .editCategory(this.categoryId, this.model)
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
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }
}
