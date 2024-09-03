import { Component, OnDestroy } from '@angular/core';
import { AddUser } from '../models/add-user.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent implements OnDestroy {
  model: AddUser;
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

  onAddUserSubmit() {
    this.addUserSubscription = this.userService
      .addUser(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/users');
        },
      });
  }

  ngOnDestroy(): void {
    this.addUserSubscription?.unsubscribe();
  }
}
