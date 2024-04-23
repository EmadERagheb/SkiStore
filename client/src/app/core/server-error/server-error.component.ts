import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss'],
})
export class ServerErrorComponent {
  error: any;
  isDevelopment:boolean=environment.production
  constructor(private router: Router) {
    this.error = this.router.getCurrentNavigation()?.extras?.state?.['error'];
  }
}
