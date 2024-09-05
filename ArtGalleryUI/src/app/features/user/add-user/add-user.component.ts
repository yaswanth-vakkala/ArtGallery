import { Component, OnDestroy } from '@angular/core';
import { AddUser } from '../models/add-user.model';
import { Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../services/user.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [FormsModule, NgOptimizedImage, RouterLink,NgIf],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnDestroy {
  model: AddUser;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  successMessage: boolean = false;
  private addUserSubscription?: Subscription;

  constructor(
    private userService: UserService,
    private router: Router,
  ) {
    this.model = {
      email: '',
      password: '',
      firstName: '',
      lastName: '',
      phoneNumber: '',
      countryCode: '',
      isAdmin: false
    };
  }

  onAddUserSubmit(form:NgForm) {
    this.isFormSubmitted = true;
    if(this.model.email && this.model.password && this.model.firstName && form.valid){
      this.addUserSubscription = this.userService
      .addUser(this.model)
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
    this.addUserSubscription?.unsubscribe();
  }
}
