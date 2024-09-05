import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AddressList } from '../models/address-list.model';
import { Observable, Subscription } from 'rxjs';
import { AddressService } from '../services/address.service';

@Component({
  selector: 'app-address-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe],
  templateUrl: './address-list.component.html',
  styleUrl: './address-list.component.css'
})
export class AddressListComponent implements OnInit, OnDestroy{
  userId:any= '';
  model?: AddressList[];
  @Input() parentComponent = '';
  @Output() addressSelectEvent = new EventEmitter<AddressList>();
  selectedAddress? : AddressList;
  private getAddressesByUserIdSubscription?: Subscription;
  private paramsSubscription?: Subscription;
  private deleteAddressSubscription?: Subscription;

  constructor(
    private addressService: AddressService,
    private router: Router,
    private route: ActivatedRoute
  ){}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.userId=params.get('userId');
        if(this.userId){
          this.addressService.getAddressesByUserId(this.userId).subscribe({
            next: (response) => {
              this.model=response;
            },
          });
        }
      },
    });
  }

  onAddressSelect(address:AddressList ){
    this.selectedAddress = address;
    this.addressSelectEvent.emit(address);
  }

  onDeleteClick(id: string) {
    if(confirm("Are you to delete the address?")){
      this.deleteAddressSubscription = this.addressService
      .deleteAddress(id)
      .subscribe({
        next: (response) => {
          this.router
          .navigateByUrl('/', { skipLocationChange: true })
          .then(() => {
            this.router.navigate([`user/address/${this.userId}`]);
          });
        },
      });
    }
    }
    
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.getAddressesByUserIdSubscription?.unsubscribe();
    this.deleteAddressSubscription?.unsubscribe();
  }

}
