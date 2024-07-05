import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/shared/models/order/order';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss']
})
export class CheckoutSuccessComponent {
  order?:Order
constructor(private router:Router){
  const navigate = this.router.getCurrentNavigation();
  this.order=navigate?.extras?.state as Order
}
}
