import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';



@Component({
  selector: 'app-error-test',
  templateUrl: './error-test.component.html',
  styleUrls: ['./error-test.component.scss'],
})
export class ErrorTestComponent {
  baseUrl = environment.apiUrl;
  validationsErrors:string[]=[]
  constructor(private httpClient: HttpClient) {}
  get404Error() {
    
    this.httpClient.get(this.baseUrl + 'Buggy/notFound').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }
  get500Error() {
    console.log(this.baseUrl)
    this.httpClient.get(this.baseUrl + 'Buggy/serverError').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }
  get400ValidationError() {
    this.httpClient.get(this.baseUrl + 'products/fortyTwo').subscribe({
      next: (response) => console.log(response),
      error: (error) => {
        this.validationsErrors=error.errors
        console.log(this.validationsErrors);}
    });
  }
  get400Error() {
    this.httpClient.get(this.baseUrl + 'Buggy/badRequest').subscribe({
      next: (response) => console.log(response),
      error: (error) => console.log(error),
    });
  }
}
