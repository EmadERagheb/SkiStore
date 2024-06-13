import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { LoadingService } from '../services/loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private loadingService: LoadingService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (
      !request.url.includes('isEmailExists') ||
      (request.method === 'POST' && request.url.includes('Orders'))
    ) {
      return next.handle(request);
    }
    this.loadingService.busy();
    return next.handle(request).pipe(
      delay(1000),
      finalize(() => this.loadingService.idle())
    );
  }
}
