import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/deliveryMethod';
import { map } from 'rxjs';

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
}
