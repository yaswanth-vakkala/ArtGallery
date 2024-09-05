import { AsyncPipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AppUser } from '../models/appUser.model';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe, FormsModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit, OnDestroy {
  users$?: Observable<AppUser[]>;
  userCount : number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';
  private deleteUserSubscription?: Subscription;

  constructor(private router: Router, private userService: UserService){}

  ngOnInit(): void {
    this.userService.getUserCount().subscribe({
      next: (res) => {
        this.userCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      }
    });
    this.users$ = this.userService.getAllUsers( undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize,);
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.users$ = this.userService.getAllUsers(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.users$ = this.userService.getAllUsers(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.users$ = this.userService.getAllUsers(
      this.query,
      this.sortBy,
      this.sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  search(query: string) {
    this.users$ = this.userService.getAllUsers(
      query,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize,
    );
    this.query = query;
    this.userService.getUserCount(query).subscribe({
      next: (res) => {
        this.userCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.users$ = this.userService.getAllUsers(
      this.query,
      sortBy,
      sortOrder,
      this.pageNumber,
      this.pageSize,
    );
  }

  clearFilters() {
    window.location.reload();
    // this.router
    //   .navigateByUrl('/admin/categories', { skipLocationChange: true })
    //   .then(() => {
    //     this.router.navigate([`/`]);
    //   });
  }

  onDeleteClick(id: string) {
    if(confirm("Are you to delete the user?")){
      this.deleteUserSubscription = this.userService
      .deleteUser(id)
      .subscribe({
        next: (response) => {
          this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate(['admin/users']);
          });
        },
      });
    }
  }

  ngOnDestroy(): void {
    this.deleteUserSubscription?.unsubscribe();
  }
}
