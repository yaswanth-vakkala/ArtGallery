import { Component, OnDestroy } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  NgForm,
  Validators,
} from '@angular/forms';
import { Address } from '../models/address.model';
import { AddAddress } from '../models/add-address.model';
import { Subscription } from 'rxjs';
import { AddressService } from '../services/address.service';
import { Router } from '@angular/router';
import { Location, NgIf } from '@angular/common';

@Component({
  selector: 'app-add-address',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './add-address.component.html',
  styleUrl: './add-address.component.css',
})
export class AddAddressComponent implements OnDestroy {
  model!: AddAddress;
  errMessage: boolean = false;
  successMessage: boolean = false;
  isFormSubmitted: boolean = false;
  private addAddressSubscription: Subscription | undefined;
  constructor(
    private addressService: AddressService,
    private router: Router,
    private _location: Location,
  ) {
    this.model = {
      addressLine: '',
      pinCode: '',
      city: '',
      landmark: '',
      country: '',
      countryCode: '',
      phoneNumber: '',
      userEmail: '',
    };
  }
  onAddAddressSubmit(form: NgForm) {
    this.isFormSubmitted = true;
    this.model.userEmail = localStorage.getItem('user-email');
    if (
      this.model.userEmail &&
      this.model.addressLine &&
      this.model.pinCode &&
      this.model.city &&
      this.model.country &&
      this.model.countryCode &&
      this.model.phoneNumber &&
      form.valid
    ) {
      this.addAddressSubscription = this.addressService
        .addAddress(this.model)
        .subscribe({
          next: (response) => {
            this._location.back();
            this.successMessage = true;
            setTimeout(() => {
              this.successMessage = false;
            }, 5000);
          },
          error: (response) => {
            this.router.navigateByUrl('/user/address/add');
            this.errMessage = true;
            setTimeout(() => {
              this.errMessage = false;
            }, 5000);
          },
        });
    }
  }

  ngOnDestroy(): void {
    this.addAddressSubscription?.unsubscribe();
  }
}
