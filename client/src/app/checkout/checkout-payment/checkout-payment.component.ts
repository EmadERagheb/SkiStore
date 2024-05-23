import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/address';
import { OrderToCreate } from 'src/app/shared/models/order/order-to-create';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;
  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastrService: ToastrService,
    private router:Router
  ) {}
  submitOrder() {
    const basket = this.basketService.CurrentBasket;
    if (!basket) return null;
    const orderToCreate = this.getOrderToCreate(basket);

    if (!orderToCreate) return null;
    return this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order) => {
        this.toastrService.show('order create');
        this.basketService.deleteLocalBasket()
        const navigationExtras:NavigationExtras= {state:order}
        this.router.navigate(['checkout/success'],navigationExtras)
      },
    });
  }
  get deliveryForm() {
    return this.checkoutForm?.get('deliveryForm');
  }
  get paymentForm() {
    return this.checkoutForm?.get('paymentForm');
  }
  get addressForm() {
    return this.checkoutForm?.get('addressForm');
  }
  getOrderToCreate(basket: Basket) {
    const deliveryMethodId = this.deliveryForm?.get('deliveryMethod')?.value;
    const shippingAddress = this.addressForm?.value as Address;
    if (!deliveryMethodId || !shippingAddress) return null;
    return { basketId: basket.id, deliveryMethodId, shippingAddress };
  }
}
