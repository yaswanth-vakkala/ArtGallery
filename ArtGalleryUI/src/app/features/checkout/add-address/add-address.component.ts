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
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';

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
    private cookieService: CookieService,
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

  goBack(){
    this._location.back();
  }

  onAddAddressSubmit(form: NgForm) {
    this.isFormSubmitted = true;
    let token=this.cookieService.get('Authorization');
    const decodedToken: any=jwtDecode(token);
    let userEmail: string =
    decodedToken[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
    ];
    //this.model.userEmail = localStorage.getItem('user-email');
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
