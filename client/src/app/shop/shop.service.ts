import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Paging } from '../shared/models/Paging';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';
import { Observable, map, of } from 'rxjs';
import { ShopPrams } from '../shared/models/shop-prams';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;

  brands: Brand[] = [];
  productTypes: ProductType[] = [];
  paging?: Paging<Product[]>;
  productCache = new Map<string,Paging<Product[]>>();
  private shopParams = new ShopPrams();
  constructor(private httpClient: HttpClient) {}
  getProducts(useCache = true): Observable<Paging<Product[]>> {
    if (!useCache) this.productCache = new Map();
    if (this.productCache.size > 0 && useCache) {
      if (this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.paging = this.productCache.get(
          Object.values(this.shopParams).join('-')
        );
        if (this.paging) return of(this.paging);
      }
    }
    let params = new HttpParams();
    params = params.append('PageSize', this.shopParams.pageSize);
    params = params.append('PageIndex', this.shopParams.pageIndex);
    params = params.append('Sort', this.shopParams.sort);
    if (this.shopParams.brandId > 0)
      params = params.append('BrandId', this.shopParams.brandId);
    if (this.shopParams.productTypeId > 0)
      params = params.append('ProductTypeId', this.shopParams.productTypeId);
    if (this.shopParams.search)
      params = params.append('Search', this.shopParams.search);
    return this.httpClient
      .get<Paging<Product[]>>(this.baseUrl + 'Products', { params })
      .pipe(
        map((response) => {
          this.productCache.set(
            Object.values(this.shopParams).join('-'),
            response
          );

          this.paging = response;
          return response;
        })
      );
  }
  setShopParams(params: ShopPrams) {
    this.shopParams = params;
  }
  getShopParams() {
    return this.shopParams;
  }
  getProduct(id: number) {
    console.log(this.productCache);
    const product = [...this.productCache.values()].reduce(
      (acc, pagingResult) => {
        return { ...acc, ...pagingResult.data.find((x) => x.id === id) };
      },
      {} as Product
    );
    if(Object.keys(product).length!==0) return of(product)
    return this.httpClient.get<Product>(this.baseUrl + 'Products/' + id);
  }
  getBrands() {
    if (this.brands.length > 0) return of(this.brands);
    return this.httpClient.get<Brand[]>(this.baseUrl + 'Brands').pipe(
      map((response) => {
        this.brands;
        return response;
      })
    );
  }
  getProductType() {
    if (this.productTypes.length > 0) return of(this.productTypes);
    return this.httpClient
      .get<ProductType[]>(this.baseUrl + 'ProductTypes')
      .pipe(
        map((response) => {
          this.productTypes = response;
          return response;
        })
      );
  }
}
