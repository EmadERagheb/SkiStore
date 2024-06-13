import * as cuid from 'cuid';

export interface Basket {
  id: string;
  items: BasketItem[];
  deliveryMethodId?: number;
  clientSecret?: string;
  PaymentIntentId?: string;
  shippingPrice: number;
}

export interface BasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureURL: string;
  type: string;
}
export class Basket implements Basket {
  id: string = cuid();
  items: BasketItem[] = [];
  shippingPrice = 0;
}
export interface BasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}
