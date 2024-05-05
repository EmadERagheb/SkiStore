import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AccountService } from '../account.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder,private accountService :AccountService) {
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
    console.log(this.email.value)
    console.log(this.password.value)
   this.accountService.login(login).subscribe({
    next:(data)=>{
      console.log(data)
    }
   })
   
  }
}
