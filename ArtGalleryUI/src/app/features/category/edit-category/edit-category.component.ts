import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EditCategory } from '../models/edit-category.model';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css',
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  categoryId: string | null = null;
  model?: EditCategory;
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

  onEditCategorySubmit() {
    if (this.categoryId && this.model?.name) {
      this.editCategorySubscription = this.categoryService
        .editCategory(this.categoryId, this.model)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }
}
