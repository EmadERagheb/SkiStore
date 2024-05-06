import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from '../account.service';
import { Login } from 'src/app/shared/models/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder,private accountService :AccountService,private router:Router) {
    this.loginForm= fb.group(
      {
        email:[],
        password:[]
      }
    )
  }
  get email(){
    return this.loginForm.controls['email']
  }
  get password(){
    return this.loginForm.controls['password']
  }

  onSubmit(){
    const login = this.loginForm.value as Login
   this.accountService.login(login).subscribe({
    next:()=>{
     this.router.navigateByUrl('/shop')
    }
   })
   
  }
}
