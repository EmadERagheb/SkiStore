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
  shopPrams: ShopPrams;
  totalItems: number = 0;
  constructor(private shopService: ShopService) {
    this.shopPrams = this.shopService.getShopParams();
  }
  ngOnInit(): void {
    this.getProductTypes();
    this.getBrands();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: (response) => {
        this.products = response.data;
        this.totalItems = response.count;
      },
      
    });
  }
  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]),
    });
  }
  getProductTypes() {
    this.shopService.getProductType().subscribe({
      next: (response) =>
        (this.productTypes = [{ id: 0, name: 'All' }, ...response]),
    });
  }
  onBrandSelected(brandId: number) {
    const params = this.shopService.getShopParams();
    params.brandId = brandId;
    params.pageIndex = 1;
    this.shopService.setShopParams(params);
    this.shopPrams = params;
    this.getProducts();
  }
  onProductTypeSelected(productTypeId: number) {
    const params = this.shopService.getShopParams();
    params.productTypeId = productTypeId;
    params.pageIndex = 1;
    this.shopService.setShopParams(params);
    this.shopPrams = params;
    this.getProducts();
  }
  onSortSelected(event: any) {
    const params = this.shopService.getShopParams();
    params.sort = event.target.value;
    this.shopService.setShopParams(params);
    this.shopPrams = params;
    this.getProducts();
  }
  onPageChange(event: any) {
    const params = this.shopService.getShopParams();

    if (params.pageIndex != event) {
      params.pageIndex = event;
      this.shopService.setShopParams(params);
      this.shopPrams = params;
      this.getProducts();
    }
  }
  onSearch() {
    const params = this.shopService.getShopParams();

    if (this.searchInput.nativeElement.value) {
      params.search = this.searchInput.nativeElement.value;
      params.pageIndex = 1;
      this.shopService.setShopParams(params);
      this.shopPrams = params;
      this.getProducts();
    }
  }
  onReset() {
    if (this.searchInput.nativeElement.value) 
      this.searchInput.nativeElement.value = '';

      this.shopPrams = new ShopPrams();
      this.shopService.setShopParams(this.shopPrams);
      this.getProducts();
    
  }
}
