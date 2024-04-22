import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';
import { ShopPrams } from '../shared/models/shop-prams';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { every } from 'rxjs';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  productTypes: ProductType[] = [];
  sortingOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' },
  ];
  shopPrams: ShopPrams = new ShopPrams();

  totalItems: number = 0;

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProductTypes();
    this.getBrands();
    this.getProducts();
  }

  getProducts() {
    this.shopService
      .getProducts(this.shopPrams )
      .subscribe({
        next: (response) => {
          this.products = response.data;
          this.totalItems = response.count;
        },
        error: (error) => console.log(error.message),
        complete: () => console.log('product loaded'),
      });
  }
  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error.message),
      complete: () => console.log('brands loaded'),
    });
  }
  getProductTypes() {
    this.shopService.getProductType().subscribe({
      next: (response) =>
        (this.productTypes = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error.message),
      complete: () => console.log('product types loaded'),
    });
  }
  onBrandSelected(brandId: number) {
    this.shopPrams.brandId = brandId;
    this.getProducts();
  }
  onProductTypeSelected(productTypeId: number) {
    this.shopPrams.productTypeId = productTypeId;
    this.getProducts();
  }
  onSortSelected(event: any) {
    console.log('inside onSorting functions');
    this.shopPrams.sort = event.target.value;
    this.getProducts();
  }
  onPageChange(event: any) {
    console.log(event)
  if(this.shopPrams.pageIndex!=event){
    this.shopPrams.pageIndex=event;
    this.getProducts();
  }
  }
}
