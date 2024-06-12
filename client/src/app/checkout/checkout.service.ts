import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { map } from 'rxjs';
import { OrderToCreate } from '../shared/models/order/order-to-create';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseURL = environment.apiUrl;
  constructor(private httpClient: HttpClient) {}
  getDeliveryMethods() {
    return this.httpClient
      .get<DeliveryMethod[]>(`${this.baseURL}Orders/deliveryMethods`)
      .pipe(map((dm) => dm.sort((a, b) => b.price - a.price)));
  }
  createOrder(OrderToCreate: OrderToCreate) {
  return  this.httpClient.post<OrderToCreate>(this.baseURL + 'Orders', OrderToCreate);
  }
}
