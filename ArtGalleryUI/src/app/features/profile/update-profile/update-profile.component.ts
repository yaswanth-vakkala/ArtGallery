import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UpdateProfile } from './models/update-profile.model';
import { Subscription } from 'rxjs';
import { UpdateProfileService } from './services/update-profile.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css',
})
export class UpdateProfileComponent implements OnInit, OnDestroy {
  userId: string | null = null;
  model?: UpdateProfile;
  successMessage: boolean = false;
  errMessage: boolean = false;
  isFormSubmitted: boolean = false;
  private updateProfileSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private updateProfileService: UpdateProfileService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId = params.get('userId');
        if (this.userId) {
          this.updateProfileService.getuserById(this.userId).subscribe({
            next: (response) => {
              this.model = response;
              console.log(this.model);
            },
          });
        }
      },
    });
  }

  onUpdateProfileSubmit(form: NgForm) {
    this.isFormSubmitted = true;
    if (this.userId && this.model?.firstName && form.valid) {
      this.updateProfileSubscription = this.updateProfileService
        .updateProfile(this.userId, this.model)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/user/update-profile');
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

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.updateProfileSubscription?.unsubscribe();
  }
}
