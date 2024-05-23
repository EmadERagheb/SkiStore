import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { Address } from 'src/app/shared/models/address';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit {
  // This is the form group that contains all of our individual controls
  //for each field in the checkout process (e.g., first name, last name, street address, etc
  @Input() checkoutForm?: FormGroup;
  constructor(
    private accountService: AccountService,
    private toastr:ToastrService
  ) {}
  ngOnInit(): void {
    this.getUserAddress();
  }
  get addressForm() {
    return this.checkoutForm?.get('addressForm');
  }
  private getUserAddress() {
    this.accountService.getUserAddress().subscribe({
      next: (data) => this.addressForm?.patchValue(data),
    });
  }
  updateUserAddress(){
    let address = this.addressForm?.value as Address
    console.log(address)
    this.accountService.updateUserAddress(address).subscribe({
      next:(address)=> {
        this.addressForm?.reset(address)
        this.toastr.success('address updated success','success')
      },
    })
  }
}
