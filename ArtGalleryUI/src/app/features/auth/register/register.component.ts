import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RegisterRequest } from '../models/register-request.model';
import { AuthService } from '../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: RegisterRequest;
  confirmPassword: string = '';
  errMessage: boolean = false;
  successMessage: boolean = false;
  isFormSubmitted: boolean = false;
  passwordMismatch: boolean = false;
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {
    this.model = {
      email: '',
      password: '',
      firstName: '',
      lastName: '',
    };
  }

  onFormSubmit(form: NgForm) {
    this.isFormSubmitted = true;
    this.passwordMismatch = this.model.password !== this.confirmPassword;
    if (
      !this.model.email ||
      !this.model.password ||
      !this.model.firstName ||
      this.passwordMismatch ||
      form.invalid
    ) {
      return;
    }

    this.authService.register(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/login');
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
      },
    });
  }
}
