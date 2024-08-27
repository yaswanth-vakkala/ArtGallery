import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterRequest } from '../models/register-request.model';
import { AuthService } from '../services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: RegisterRequest;
  confirmPassword: string | undefined;
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

  onFormSubmit() {
    if (this.model.password === this.confirmPassword) {
      this.authService.register(this.model).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/login');
        },
      });
    }
  }
}
