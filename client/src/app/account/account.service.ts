import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from '../shared/models/login';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseURL: string = environment.apiUrl + 'Accounts/';
  private userSource = new BehaviorSubject<null | User>(null);
  public userSource$ = this.userSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}
  getCurrentUser(token: string) {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8');
    headers = headers.set('Authorization', `Bearer ${token}`);
    console.log(headers)
    return this.httpClient.get<User>(this.baseURL + 'getCurrentUser', { headers })
  .pipe(map((user) => {
    localStorage.setItem('token', user.token);
       this.userSource.next(user);
       return user
        })
      );
  }
  login(login: Login) {
    return this.httpClient.post<User>(`${this.baseURL}Login`, login).pipe(
      map((user) => {
        this.userSource.next(user);
        localStorage.setItem('token', user.token);
      })
    );
  }
  logout() {
    localStorage.removeItem('token');
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }
  // isEmailExists?email=dd
  checkEmailExists(email: string) {
    this.httpClient.get<boolean>(this.baseURL + 'isEmailExists?email' + email);
  }
}
