import { AsyncPipe } from '@angular/common';
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
  imports: [RouterLink, AsyncPipe, FormsModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css',
})
export class UserListComponent implements OnInit, OnDestroy {
  users$?: Observable<AppUser[]>;
  userCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 2;
  paginationList: number[] = [];
  query: string = '';
  sortBy: string = '';
  sortOrder: string = '';
  selectedFile?: File;
  addBulkUsersResponse?: BulkAddResponse[];
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
    this.users$ = this.userService.getAllUsers(
      undefined,
      undefined,
      undefined,
      this.pageNumber,
      this.pageSize,
    );
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
    if (confirm('Are you to delete the user?')) {
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
