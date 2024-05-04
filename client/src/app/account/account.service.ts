import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseURL: string = environment.apiUrl + 'Accounts/';
  private userSource = new BehaviorSubject<null | User>(null);
  public userSource$ = this.userSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}

  login(values: any) {
    return this.httpClient.post<User>(`${this.baseURL}+Login`, values).pipe(
      map((user) => {
        this.userSource.next(user);
        localStorage.setItem('token', user.token);
      })
    );
  }
  logout(){
    localStorage.removeItem('token');
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }
  // isEmailExists?email=dd
  checkEmailExists(email:string)
  {
     this.httpClient.get<boolean>(this.baseURL+'isEmailExists?email'+email)
  }
}
