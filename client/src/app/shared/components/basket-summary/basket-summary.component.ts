import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketItem } from '../../models/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss'],
})
export class BasketSummaryComponent {
  @Output() addItem = new EventEmitter<BasketItem>();
  @Output() removeItem = new EventEmitter<{ item: BasketItem; quantity: number }>();
  @Input () isBasket= true
  constructor(public basketService: BasketService) {}
  addBasketItem(item: BasketItem) {
    this.addItem.emit(item);
  }
  removeBasketItem(item: BasketItem, quantity: number) {
    this.removeItem.emit({ item, quantity });
  }
}
