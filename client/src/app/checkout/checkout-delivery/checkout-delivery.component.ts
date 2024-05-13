import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';
import { DeliveryMethod } from 'src/app/shared/models/deliveryMethod';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  
  @Input() checkoutForm?:FormGroup;
  deliverMethods:DeliveryMethod[]=[]
  
 constructor(private checkoutService:CheckoutService){}

  ngOnInit(): void {
  this.checkoutService.getDeliveryMethods().subscribe(
    {
      next:dms=>this.deliverMethods=dms
    }
  )
  }
}
