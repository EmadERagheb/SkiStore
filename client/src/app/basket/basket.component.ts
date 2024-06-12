import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent {
  constructor(public basketService: BasketService) {}
  onIncrement(event : BasketItem, quantity: number) {
    this.basketService.addItemToBasket(event, quantity);
  }
  onDecrement(event:{item: BasketItem, quantity: number}) {
    this.basketService.removeItemFromBasket(event.item.id,event. quantity);
  }
}
