import { Injectable, model} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Address } from '../models/address.model';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { AddAddress } from '../models/add-address.model';
import { UpdateAddress } from '../models/update-address.model';
import { AddressList } from '../models/address-list.model';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  constructor(private http: HttpClient, private cookieService: CookieService) {}

    getAllAddresses(): Observable<Address[]>{
      return this.http.get<Address[]>(`${environment.apiBaseUrl}/api/address?addAuth=true`);
    }

    getAddressById(addressId: string): Observable<Address> {
      return this.http.get<Address>(
        `${environment.apiBaseUrl}/api/address/${addressId}?addAuth=true`
      );
    }

    getAddressesByUserId(userId:string): Observable<AddressList[]>{
      return this.http.get<AddressList[]>(
        `${environment.apiBaseUrl}/api/address/appuser/${userId}?addAuth=true`
      );
    }

    addAddress(model: AddAddress): Observable<void> {
      return this.http.post<void>(
        `${environment.apiBaseUrl}/api/address?addAuth=true`,
        model
      );
    }

    updateAddress(addressId: string, model: UpdateAddress): Observable<Address> {
      return this.http.put<Address>(
        `${environment.apiBaseUrl}/api/address/${addressId}?addAuth=true`,
        model
      );
    }
    deleteAddress(addressId: string): Observable<boolean> {
      return this.http.delete<boolean>(
        `${environment.apiBaseUrl}/api/address/${addressId}?addAuth=true`
      );
    }

  }
