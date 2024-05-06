import { Component } from '@angular/core';
import {
  AbstractControl,
  AbstractControlOptions,
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { Register } from 'src/app/shared/models/register';
import { debounce, debounceTime, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  passwordPattern: string =
    "(?=^.{6,10}$)(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*s).*$";
  registerFrom = this.fb.group({
    displayName: ['', [Validators.required]],
    email: [
      '',
      [Validators.required, Validators.email],
      [this.validateEmailIfExist()],
    ],
    password: [
      '',
      [Validators.required, Validators.pattern(this.passwordPattern)],
    ],
  });
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountService: AccountService
  ) {}
  onSubmit() {
    const register = this.registerFrom.value as Register;
    this.accountService.register(register).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
    });
  }
  validateEmailIfExist(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.accountService
            .checkEmailExists(control.value)
            .pipe(map((result) => (result ? { emailExists: true } : null)));
        })
      );
    };
  }
}
