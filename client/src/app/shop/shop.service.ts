import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Paging } from '../shared/models/Paging';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';
import { ShopPrams } from '../shared/models/shop-prams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:7291/api/';
  constructor(private httpClient: HttpClient) {}
  getProducts(shopPrams:ShopPrams) {
    let params = new HttpParams();
    params = params.append('PageSize', shopPrams.pageSize);
    params=params.append('PageIndex',shopPrams.pageIndex)
    params=params.append('Sort',shopPrams.sort)
    if (shopPrams.brandId>0) params = params.append('BrandId', shopPrams.brandId);
    if (shopPrams.productTypeId>0)params=  params.append('ProductTypeId', shopPrams.productTypeId);
    if(shopPrams.search) params=params.append("Search",shopPrams.search)
    return this.httpClient.get<Paging<Product[]>>(this.baseUrl + 'Products', {params});
  }
  getBrands() {
    return this.httpClient.get<Brand[]>(this.baseUrl + 'Brands');
  }
  getProductType() {
    return this.httpClient.get<ProductType[]>(this.baseUrl + 'ProductTypes');
  }
}
