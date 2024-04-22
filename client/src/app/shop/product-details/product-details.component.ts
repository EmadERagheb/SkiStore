import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/Product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;
  constructor(
    private shopService: ShopService,
    private activeRouter: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.activeRouter.snapshot.paramMap.get('id');
    console.log(id)
    if (id) this.shopService.getProduct(+id).subscribe({
      next: (prod) => this.product=prod,
      error:(error)=>console.log(error),
      complete:()=>console.log('product loaded')
    }
    );
  }
}