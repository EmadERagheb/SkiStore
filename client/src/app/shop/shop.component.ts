import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { ProductType } from '../shared/models/ProductType';
import { ShopPrams } from '../shared/models/shop-prams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchInput!: ElementRef;
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
    this.shopService.getProducts(this.shopPrams).subscribe({
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
    this.shopPrams.pageIndex=1
    this.getProducts();
  }
  onProductTypeSelected(productTypeId: number) {
    this.shopPrams.productTypeId = productTypeId;
    this.shopPrams.pageIndex=1
    this.getProducts();
  }
  onSortSelected(event: any) {
    console.log('inside onSorting functions');
    this.shopPrams.sort = event.target.value;
    this.getProducts();
  }
  onPageChange(event: any) {
    console.log(event);
    if (this.shopPrams.pageIndex != event) {
      this.shopPrams.pageIndex = event;
      this.getProducts();
    }
  }
  onSearch() {
    if (this.searchInput.nativeElement.value) {
      this.shopPrams.search = this.searchInput.nativeElement.value;
      this.shopPrams.pageIndex=1
      this.getProducts();
    }
  }
  onReset() {
    if (this.searchInput.nativeElement.value) {
      this.searchInput.nativeElement.value = '';
      this.shopPrams = new ShopPrams();
      this.getProducts();
    }
  }
}
