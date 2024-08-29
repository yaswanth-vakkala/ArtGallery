import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpdateProfile } from './models/update-profile.model';
import { Subscription } from 'rxjs';
import { UpdateProfileService } from './services/update-profile.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent implements OnInit, OnDestroy{
  userId: string | null =null;
  model?: UpdateProfile;
  private updateProfileSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private updateProfileService: UpdateProfileService,
    private router: Router,
    private route: ActivatedRoute
  ){}
 
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId = params.get('userId');
        if (this.userId) {
          this.updateProfileService.getuserById(this.userId).subscribe({
            next: (response) => {
              this.model = response;
              console.log(this.model)
            },
          });
        }
      },
    });
  }

  onUpdateProfileSubmit() {
    if (this.userId && (this.model?.firstName || this.model?.lastName || this.model?.countryCode || this.model?.phoneNumber)) {
      this.updateProfileSubscription = this.updateProfileService
        .updateProfile(this.userId, this.model)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/user/update-profile');
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.updateProfileSubscription?.unsubscribe();
  }
}
