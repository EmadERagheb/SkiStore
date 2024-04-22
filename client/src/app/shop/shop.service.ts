import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Paging } from '../shared/models/Paging';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7291/api/';
  constructor(private httpClient: HttpClient) {}
  getProducts(brandId?: number, productTypeId?: number,sort?:string) {
    let params = new HttpParams();
    params = params.append('PageSize', 50);
    if (brandId) params = params.append('BrandId', brandId);
    if (productTypeId)params=  params.append('ProductTypeId', productTypeId);
    if(sort) params=params.append('Sort',sort)
    return this.httpClient.get<Paging<Product[]>>(this.baseUrl + 'Products', {params});
  }
  getBrands() {
    return this.httpClient.get<Brand[]>(this.baseUrl + 'Brands');
  }
  getProductType() {
    return this.httpClient.get<ProductType[]>(this.baseUrl + 'ProductTypes');
  }
}
