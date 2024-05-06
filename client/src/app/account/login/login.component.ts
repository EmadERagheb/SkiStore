import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Login } from 'src/app/shared/models/login';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });
  returnUrl: string;
  constructor(
    private accountService: AccountService,
    private router: Router,
    private activateRouter: ActivatedRoute
  ) {
    this.returnUrl = this.activateRouter.snapshot.queryParams['returnUrl'];
  }
  get email() {
    return this.loginForm.controls['email'];
  }
  get password() {
    return this.loginForm.controls['password'];
  }

  onSubmit() {
    const login = this.loginForm.value as Login;
    this.accountService.login(login).subscribe({
      next: () => {
        if (this.returnUrl) this.router.navigate([this.returnUrl]);
        else this.router.navigateByUrl('/shop');
      },
    });
  }
}
