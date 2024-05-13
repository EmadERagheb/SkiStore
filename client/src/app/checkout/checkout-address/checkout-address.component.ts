import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent {
  // This is the form group that contains all of our individual controls
  //for each field in the checkout process (e.g., first name, last name, street address, etc
  @Input() checkoutForm?: FormGroup;
}
