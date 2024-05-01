import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/Product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseURL: string = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotals = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotals.asObservable();

  constructor(private httpClient: HttpClient) {}
  getBasket(id: string) {
    this.httpClient.get<Basket>(this.baseURL + 'Baskets?id=' + id).subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
        this.calculateTotal();
      },
    });
  }
  deleteBasket(id: string) {
    return this.httpClient.delete(this.baseURL + 'Baskets?id=' + id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotals.next(null);
        localStorage.removeItem('basket_id');
      },
    });
  }
  setBasket(basket: Basket) {
    this.httpClient.post<Basket>(this.baseURL + 'Baskets', basket).subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
        this.calculateTotal();
      },
    });
  }
  get CurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: Product | BasketItem, quantity: number = 1) {
    if (this.IsProduct(item)) item = this.mapProductToBasketItem(item);
    const basket = this.CurrentBasket ?? this.createBasket();
    basket.items = this.addOrUpdatedItemToBasket(basket.items, item, quantity);
    this.setBasket(basket);
  }
  removeItemFromBasket(id: number, quantity = 1) {
    const basket = this.CurrentBasket;
    if (basket) {
      const item = basket.items.find((q) => q.id == id);
      if (item) {
        item.quantity -= quantity;
        if (item.quantity === 0) {
          basket.items = basket.items.filter((x) => x !== item);
        }
        if (basket.items.length > 0) this.setBasket(basket);
        else this.deleteBasket(basket.id);
      }
    }
  }
  addOrUpdatedItemToBasket(
    items: BasketItem[],
    itemToAddToBasket: BasketItem,
    quantity: number
  ): BasketItem[] {
    const item = items.find((q) => q.id == itemToAddToBasket.id);
    if (item) {
      item.quantity += quantity;
    } else {
      itemToAddToBasket.quantity = quantity;
      items.push(itemToAddToBasket);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }
  private mapProductToBasketItem(product: Product): BasketItem {
    return {
      id: product.id,
      productName: product.name,
      pictureURL: product.pictureUrl,
      type: product.productType,
      price: product.price,
      quantity: 0,
    };
  }
  private calculateTotal() {
    if (this.CurrentBasket) {
      const shipping = 0;
      const subTotal = this.CurrentBasket.items.reduce(
        (a, b) => b.price * b.quantity + a,
        0
      );
      const total = shipping + subTotal;
      this.basketTotals.next({ total, shipping, subTotal });
    }
  }
  private IsProduct(item: Product | BasketItem): item is Product {
    return !!(item as Product).brand;
  }
}
