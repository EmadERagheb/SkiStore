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
  onIncrement(item: BasketItem, quantity: number) {
    this.basketService.addItemToBasket(item, quantity);
  }
  onDecrement(item: BasketItem, quantity: number) {
    this.basketService.removeItemFromBasket(item.id, quantity);
  }
}
