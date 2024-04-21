import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Paging } from '../shared/models/Paging';
import { Product } from '../shared/models/Product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
baseUrl="https://localhost:7291/"
  constructor(private httpClient:HttpClient) { }
  getProducts(){
    return this.httpClient.get<Paging<Product[]>>(this.baseUrl+"api/Products/?PageSize=50");
  }
}
