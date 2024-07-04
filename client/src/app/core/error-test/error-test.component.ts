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
  validationsErrors: string[] = [];
  constructor(private httpClient: HttpClient) {}
  get404Error() {
    this.httpClient.get(this.baseUrl + 'Buggy/notFound').subscribe({});
  }
  get500Error() {
    this.httpClient.get(this.baseUrl + 'Buggy/serverError').subscribe({});
  }
  get400ValidationError() {
    this.httpClient.get(this.baseUrl + 'products/fortyTwo').subscribe({
      error: (error) => {
        this.validationsErrors = error.errors;
      },
    });
  }
  get400Error() {
    this.httpClient.get(this.baseUrl + 'Buggy/badRequest').subscribe({});
  }
}
