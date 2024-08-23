import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterRequest } from '../models/register-request.model';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: RegisterRequest;
  constructor(private authService: AuthService, private router: Router) {
    this.model = {
      email: '',
      password: '',
    };
  }

  onFormSubmit() {
    this.authService.register(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/login');
      },
    });
  }
}
