import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterLink } from '@angular/router';
import { NgIf, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgOptimizedImage, RouterLink, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  model: LoginRequest;
  errMessage: boolean = false;
  isFormSubmitted:boolean = false;
  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router,
  ) {
    this.model = {
      email: '',
      password: '',
    };
  }

  onFormSubmit(form:NgForm): void {
    this.isFormSubmitted = true;
    if(!this.model.email || !this.model.password || form.invalid){
      return;
    }
    this.authService.login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set(
          'Authorization',
          `Bearer ${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict',
        );
        this.authService.setUser({
          email: response.email,
          roles: response.roles,
          id: response.id
        });
        this.router.navigateByUrl('/');
      },
      error: (res) => {
        this.errMessage = true;
        setTimeout(() =>{
          this.errMessage=false;
        },5000);
      }
    });
  }
}
