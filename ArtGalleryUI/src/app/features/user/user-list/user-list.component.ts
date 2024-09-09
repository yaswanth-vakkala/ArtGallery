import { AsyncPipe, DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AppUser } from '../models/appUser.model';
import { UserService } from '../services/user.service';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { BulkAddResponse } from '../models/bulk-add-response';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe, FormsModule, DatePipe],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css',
})
export class UserListComponent implements OnInit, OnDestroy {
  users?: AppUser[];
  userCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';
  selectedFile?: File;
  errMessage: boolean = false;
  addBulkUsersResponse?: BulkAddResponse[];
  selectedUsers: Set<string> = new Set<string>();
  private deleteUserSubscription?: Subscription;

  constructor(
    private router: Router,
    private userService: UserService,
    private activateRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.userService.getUserCount().subscribe({
      next: (res) => {
        this.userCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
    this.userService
      .getAllUsers(
        undefined,
        undefined,
        undefined,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.users = res;
        },
      });
  }

  toggleSelection(user: any) {
    if (this.selectedUsers.has(user.id)) {
      this.selectedUsers.delete(user.id);
    } else {
      this.selectedUsers.add(user.id);
    }
  }

  isSelected(user: any): boolean {
    return this.selectedUsers.has(user.id);
  }

  toggleSelectAll(event: any) {
    if (event.target.checked) {
      this.users?.forEach((user) => this.selectedUsers.add(user.id));
    } else {
      this.selectedUsers.clear();
    }
  }

  areAllSelected(): boolean {
    return this.users?.length === this.selectedUsers.size;
  }

  deleteSelectedUsers() {
    if (confirm('Are you sure to delete the selected users?')) {
      const userIdsArray = Array.from(this.selectedUsers);
      this.userService.deleteUsersBulk(userIdsArray).subscribe({
        next: (res) => {
          this.userService
            .getAllUsers(
              this.query,
              this.sortBy,
              this.sortOrder,
              this.pageNumber,
              this.pageSize,
            )
            .subscribe({
              next: (res) => {
                this.users = res;
              },
            });
          this.selectedUsers.clear();
        },
      });
    }
  }

  getPage(pageNumber: number) {
    this.pageNumber = pageNumber;
    this.userService
      .getAllUsers(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.users = res;
        },
      });
  }

  getPreviousPage() {
    if (this.pageNumber - 1 < 1) {
      return;
    }
    this.pageNumber -= 1;
    this.userService
      .getAllUsers(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.users = res;
        },
      });
  }

  getNextPage() {
    if (this.pageNumber + 1 > this.paginationList.length) {
      return;
    }
    this.pageNumber += 1;
    this.userService
      .getAllUsers(
        this.query,
        this.sortBy,
        this.sortOrder,
        this.pageNumber,
        this.pageSize,
      )
      .subscribe({
        next: (res) => {
          this.users = res;
        },
      });
  }

  search(query: string) {
    this.userService
      .getAllUsers(query, undefined, undefined, 1, this.pageSize)
      .subscribe({
        next: (res) => {
          this.users = res;
        },
      });
    this.query = query;
    this.userService.getUserCount(query).subscribe({
      next: (res) => {
        this.pageNumber = 1;
        this.userCount = res;
        this.paginationList = new Array(Math.ceil(res / this.pageSize));
      },
    });
  }

  sort(sortBy: string, sortOrder: string) {
    this.sortBy = sortBy;
    this.sortOrder = sortOrder;
    this.userService
      .getAllUsers(this.query, sortBy, sortOrder, 1, this.pageSize)
      .subscribe({
        next: (res) => {
          this.pageNumber = 1;
          this.users = res;
        },
      });
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
    if (confirm('Are you sure to delete the user?')) {
      this.deleteUserSubscription = this.userService.deleteUser(id).subscribe({
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

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  uploadFile() {
    const maxFileSize = 4 * 1024 * 1024;
    if (this.selectedFile) {
      const validTypes = [
        'application/vnd.ms-excel',
        'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      ];

      if (!validTypes.includes(this.selectedFile.type)) {
        alert('Please upload a valid Excel file (.xls or .xlsx).');
        return;
      }

      if (this.selectedFile.size > maxFileSize) {
        alert('File size exceeds 4MB. Please upload a smaller file.');
        return;
      }
      this.userService.addUsersBulk(this.selectedFile).subscribe({
        next: (res) => {
          this.addBulkUsersResponse = res;
        },
        error: (response) => {
          this.errMessage = true;
          setTimeout(() => {
            this.errMessage = false;
          }, 5000);
        },
      });
    } else {
      alert('No file selected!');
    }
  }

  getSuccessCount(): number {
    return this.addBulkUsersResponse!.filter(
      (user) => user.status === 'success',
    ).length;
  }

  getFailedCount(): number {
    return this.addBulkUsersResponse!.filter((user) => user.status === 'failed')
      .length;
  }

  refreshPage() {
    window.location.reload();
  }
  ngOnDestroy(): void {
    this.deleteUserSubscription?.unsubscribe();
  }
}
