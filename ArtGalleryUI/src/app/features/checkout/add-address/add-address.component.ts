import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Address } from '../models/address.model';
import { AddAddress } from '../models/add-address.model';
import { Subscription } from 'rxjs';
import { AddressService } from '../services/address.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-address',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-address.component.html',
  styleUrl: './add-address.component.css'
})
export class AddAddressComponent implements OnDestroy{
  model!: AddAddress;
  private addAddressSubscription: Subscription | undefined;
  constructor(
    private addressService: AddressService,
    private router: Router
  ) {
    this.model = {
      addressLine: '',
      pinCode: '',
      city: '',
      landmark: '',
      country: '',
      countryCode: '',
      phoneNumber: '',
      userEmail: '' ,
    };
  }
  onAddAddressSubmit() {
    this.model.userEmail = localStorage.getItem('user-email')
    this.addAddressSubscription = this.addressService
      .addAddress(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('user/address/add');
        },
        error:(response)=>{
          this.router.navigateByUrl('/');
        }
      });
  }

  ngOnDestroy(): void {
    this.addAddressSubscription?.unsubscribe();
  }
}