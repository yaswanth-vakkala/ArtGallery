import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpdateAddress } from '../models/update-address.model';
import { Subscription } from 'rxjs';
import { AddressService } from '../services/address.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'app-update-address',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-address.component.html',
  styleUrl: './update-address.component.css'
})
export class UpdateAddressComponent implements OnInit,OnDestroy {
  addressId: string | null=null;
  model?:UpdateAddress;
  private updateAddressSubscription?: Subscription;
  private paramsSubscription?: Subscription;

  constructor(
    private addressService: AddressService,
    private router: Router,
    private route: ActivatedRoute,
    private _location: Location,
  ){};
  
    message: boolean=false;

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.addressId = params.get('addressId');
        if (this.addressId) {
          this.addressService.getAddressById(this.addressId).subscribe({
            next: (response) => {
              this.model = response;
              // this.toastr.success('Hello world!', 'Toastr fun!');
            },
          });
        }
      },
    });
  }

  goBack(){
    this._location.back();
  }

  onUpdateAddressSubmit(){
    const userid=localStorage.getItem('user-id');
    if (this.addressId && (this.model?.addressLine || this.model?.pinCode || this.model?.city || this.model?.landmark || this.model?.country || this.model?.countryCode || this.model?.phoneNumber) ) {
      this.updateAddressSubscription = this.addressService
        .updateAddress(this.addressId, this.model)
        .subscribe({
          next: (response) => {
            this.message=true;
            // this.toastr.success('Hello world!', 'Toastr fun!');
            this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
            this.router.navigate([`user/address/${userid}`]);
          });
            setTimeout(() =>{
              this.message=false;
            },5000);
          },
        });
    }
  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.updateAddressSubscription?.unsubscribe();
  }

}
