import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from '../shared/models/login';
import { Register } from '../shared/models/register';
import { Address } from '../shared/models/address';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseURL: string = environment.apiUrl + 'Accounts/';
  private userSource = new ReplaySubject<null | User>(1);
  public userSource$ = this.userSource.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) {}
  getCurrentUser(token: string | null) {
    if (token == null) {
      this.userSource.next(null);
      return of(null);
    } else {
      let headers = new HttpHeaders();
      headers = headers.set('Content-Type', 'application/json; charset=utf-8');
      headers = headers.set('Authorization', `Bearer ${token}`);
    
      return this.httpClient
        .get<User>(this.baseURL + 'getCurrentUser', { headers })
        .pipe(
          map((user) => {
            if (user) {
              localStorage.setItem('token', user.token);
              this.userSource.next(user);
              return user;
            } else {
              return null;
            }
          })
        );
    }
  }
  register(register: Register) {
    return this.httpClient.post<User>(this.baseURL + 'Register', register).pipe(
      map((user) => {
        this.userSource.next(user);
        localStorage.setItem('token', user.token);
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
    return this.httpClient.get<boolean>(
      this.baseURL + 'isEmailExists?email=' + email
    );
  }
  getUserAddress() {
    return this.httpClient.get<Address>(this.baseURL + 'getUserAddress');
  }
  updateUserAddress(address: Address) {
    return this.httpClient.put<Address>(
      this.baseURL + 'updateAddress',
      address
    );
  }
}
