import { Component, OnDestroy, OnInit } from '@angular/core';
import { EditUser } from '../models/edit-user.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent implements OnInit, OnDestroy {
  userId: string | null = null;
  model?: EditUser;
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

  onEditUserSubmit() {
    if (this.userId && this.model?.firstName) {
      this.editUserSubscription = this.userService
        .editUser(this.userId, this.model)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/users');
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editUserSubscription?.unsubscribe();
  }
}
