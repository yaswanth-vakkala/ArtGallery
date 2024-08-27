import { Component } from '@angular/core';
import { AddAddressComponent} from "./add-address/add-address.component";

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [AddAddressComponent],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {

}
