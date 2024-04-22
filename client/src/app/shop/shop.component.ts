import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  productTypes: ProductType[] = [];
  brandIdSelected: number = 0;
  productTypeIdSelected: number = 0;
  sortingOptions= [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' },
  ];
  sortSelected="name";

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProductTypes();
    this.getBrands();
    this.getProducts();
  }

  getProducts() {
    this.shopService
      .getProducts(this.brandIdSelected, this.productTypeIdSelected,this.sortSelected)
      .subscribe({
        next: (response) => (this.products = response.data),
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
    this.brandIdSelected = brandId;
    this.getProducts();
  }
  onProductTypeSelected(productTypeId: number) {
    this.productTypeIdSelected = productTypeId;
    this.getProducts();
  }
  onSortSelected(event:any) {
    console.log("inside onSorting functions")
    this.sortSelected=event.target.value
    this.getProducts();
  }
}
