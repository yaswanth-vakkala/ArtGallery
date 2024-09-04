import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../../features/auth/models/user.model';
import { AuthService } from '../../../features/auth/services/auth.service';
import { FormsModule } from '@angular/forms';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit {
  user?: User;
  query: string = '';
  showSearchBox: boolean = false;
  constructor(
    private authService: AuthService,
    private router: Router,
    private sharedService: SharedService,
  ) {}

  ngOnInit(): void {
    this.router.events.subscribe(() => {
      this.showSearchBox = this.router.url === '/';
    });
    this.authService.user().subscribe({
      next: (response) => {
        this.user = response;
      },
    });

    this.user = this.authService.getUser();
  }

  onQuery() {
    this.sharedService.sendMessage(this.query);
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }
}
