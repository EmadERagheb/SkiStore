import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  constructor(private basketService: BasketService, private fb: FormBuilder) {}
  ngOnInit(): void {
   this.getDeliveryMethodValue()
  }

  checkoutForm = this.fb.group({
    addressForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
    }),
    deliveryForm: this.fb.group({
      deliveryMethod: ['', Validators.required],
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required],
    }),
  });

  get deliveryForm() {
    return this.checkoutForm.get('deliveryForm');
  }
  get paymentForm() {
    return this.checkoutForm.get('paymentForm');
  }
  get addressForm() {
    return this.checkoutForm.get('addressForm');
  }

  getDeliveryMethodValue() {
    const basket = this.basketService.CurrentBasket;
    if (basket && basket.deliveryMethodId) {
      this.deliveryForm
        ?.get('deliveryMethod')
        ?.patchValue(basket.deliveryMethodId.toString());
    }
  }
}
