import { Component, OnDestroy, OnInit } from '@angular/core';
import { EditUser } from '../models/edit-user.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../services/user.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [FormsModule, RouterLink,NgIf],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent implements OnInit, OnDestroy {
  userId: string | null = null;
  model?: EditUser;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private editUserSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId = params.get('id');
        if (this.userId) {
          this.userService.getUserById(this.userId).subscribe({
            next: (response) => {
              this.model = response;
            },
          });
        }
      },
    });
  }

  onEditUserSubmit(form:NgForm) {
    this.isFormSubmitted = true;
    if (this.userId && this.model?.firstName && form.valid) {
      this.editUserSubscription = this.userService
        .editUser(this.userId, this.model)
        .subscribe({
          next: (response) => {
            this.successMessage = true;
            setTimeout(() => {
            this.successMessage = false;
            }, 5000);
            this.router.navigateByUrl('/admin/users');
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
    this.editUserSubscription?.unsubscribe();
  }
}
