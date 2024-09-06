import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css',
})
export class CategoryListComponent implements OnInit, OnDestroy {
  categories$?: Observable<Category[]>;
  private deleteCategorySubscription?: Subscription;

  constructor(
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  onDeleteClick(id: string) {
    if(confirm("Are you to delete the category?")){
      this.deleteCategorySubscription = this.categoryService
      .deleteCategory(id)
      .subscribe({
        next: (response) => {
          this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate(['admin/categories']);
          });
        },
      });
    }
    }
    
  ngOnDestroy(): void {
    this.deleteCategorySubscription?.unsubscribe();
  }
}
