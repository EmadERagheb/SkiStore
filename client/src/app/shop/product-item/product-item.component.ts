import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/Product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent {
  @Input() product?: Product;

  constructor(private basketServices: BasketService) {}
  addProductToBasket() {
    this.product && this.basketServices.addItemToBasket(this.product);
  }
}
