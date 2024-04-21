import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/Product';
import { Paging } from './models/Paging';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Ski Store';
  products:Product[]=[];

  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
    this.httpClient.get<Paging<Product[]>>('https://localhost:7291/api/Products?pageSize=50').subscribe({
      next: (data) => {
        this.products=data.data;
        console.log('Data received from server : ', this.products);
      },
      error: (error) =>
        console.log(`Error Occurred while fetching the data : ${error}`),
      complete: () => {
        console.log('Data completed');
        console.log('more work to do');
      },
    });
  }
}
