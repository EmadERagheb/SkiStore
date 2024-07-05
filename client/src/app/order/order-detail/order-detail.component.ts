import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order.service';
import { Order } from 'src/app/shared/models/order/order';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss'],
})
export class OrderDetailComponent implements OnInit {
  order?: Order;
  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private bsService: BreadcrumbService
  ) {
    this.bsService.set('@OrderDetailed', '');
  }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    id &&
      this.orderService.getOrderDetailed(+id).subscribe({
        next: (order) => {
          this.order = order;
          this.bsService.set(
            '@OrderDetailed',
            `Order# ${order.id} - ${order.status}`
          );
        },
      });
  }
}
