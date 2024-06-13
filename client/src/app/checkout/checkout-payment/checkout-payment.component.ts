import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { BasketService } from 'src/app/basket/basket.service';
import { ToastrService } from 'ngx-toastr';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/address';
import { OrderToCreate } from 'src/app/shared/models/order/order-to-create';
import { NavigationExtras, Router } from '@angular/router';
import {
  Stripe,
  StripeCardCvcElement,
  StripeCardExpiryElement,
  StripeCardNumberElement,
  loadStripe,
} from '@stripe/stripe-js';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  strip: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;
  loading = false;
  cardNumberComplete = false;
  cardExpiryComplete = false;
  cardCvcComplete = false;
  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastrService: ToastrService,
    private router: Router
  ) {}
  ngOnInit(): void {
    loadStripe(
      'pk_test_51PJiaFJ0Av2arzayHOlFz4WR0ALJpuRMXqLCxABBzmePkMEVtb2rzG7dwBgpy3wmcisOmFRUt2Fzc2ZMaZb76InE00XLtr0ys6'
    ).then((strip) => {
      this.strip = strip;
      const elements = strip?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', (event) => {
          this.cardNumberComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else {
            this.cardErrors = null;
          }
        });

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', (event) => {
          this.cardExpiryComplete = event.complete;
          if (event.error) {
            this.cardErrors = event.error.message;
          } else {
            this.cardErrors = null;
          }
        });

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', (event) => {
          this.cardCvcComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else {
            this.cardErrors = null;
          }
        });
      }
    });
  }
  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.CurrentBasket;
    if(!basket) throw new Error("basket can't be null")
    try {
      const createOrder = await this.createOrder(basket);
      const paymentResult = await this.confirmPaymentResult(basket);
      if (paymentResult.paymentIntent ) {
        this.basketService.deleteBasket(basket.id);
        const navigationExtras: NavigationExtras = { state: createOrder };
        this.router.navigate(['checkout/success'], navigationExtras);
      } else {
        this.toastrService.error(paymentResult.error.message);
      }
    } catch (error: any) {
      console.log(error);
      this.toastrService.error(error.message);
    } finally {
      this.loading = false;
    }
  }
  private async confirmPaymentResult(basket: Basket | null) {
    if (!basket) throw new Error('basket is null');
    const result = this.strip?.confirmCardPayment(basket.clientSecret!, {
      payment_method: {
        card: this.cardNumber!,
        billing_details: {
          name: this.paymentForm?.get('nameOnCard')?.value,
        },
      },
    });
    if (!result) throw new Error('problem attempting payment with strip');
    return result;
  }
  private async createOrder(basket: Basket | null) {
    if (!basket) throw new Error('basket is null');
    const orderToCreate = this.getOrderToCreate(basket);
    return firstValueFrom(this.checkoutService.createOrder(orderToCreate));
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
  getOrderToCreate(basket: Basket): OrderToCreate {
    const deliveryMethodId = this.deliveryForm?.get('deliveryMethod')?.value;
    const shippingAddress = this.addressForm?.value as Address;
    if (!deliveryMethodId || !shippingAddress)
      throw new Error('problem with basket');
    return { basketId: basket.id, deliveryMethodId, shippingAddress };
  }
  get paymentFormComplete() {
    return (
      this.paymentForm?.valid &&
      this.cardNumberComplete &&
      this.cardExpiryComplete &&
      this.cardCvcComplete
    );
  }
}
