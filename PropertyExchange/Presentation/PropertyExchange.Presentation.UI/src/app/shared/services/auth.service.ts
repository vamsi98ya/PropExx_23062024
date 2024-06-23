import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserRegistrationLogin } from '../models/userregistrationlogin.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CanActivate, Router } from '@angular/router';
import { Credentials } from '../models/credentials.model';
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { SessionTimeoutService } from './session-timeout.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {
  private currentUserSubject: BehaviorSubject<UserRegistrationLogin>;
  public currentUser: Observable<UserRegistrationLogin>;
  user: UserRegistrationLogin = new UserRegistrationLogin();
  // public getHeaders = new HttpHeaders({ 
  //   'Authorization': 'Bearer ' + String(this.currentUserValue.password)
  //  });
  constructor(private http: HttpClient,
    private router: Router,
    private cookieService:CookieService, 
    private sessionTimeoutService: SessionTimeoutService) {
  }

  canActivate(): Observable<boolean> {
    return this.sessionTimeoutService.getLogoutObservable().pipe(
      tap(loggedOut => {
        if (loggedOut) {
          // Redirect to login page if the user is logged out
          this.router.navigate(['/AuthenticateUser/Login']);
        }
      })
    );
  }

  public get currentUserValue(): UserRegistrationLogin {
    this.currentUserSubject = new BehaviorSubject<UserRegistrationLogin>(JSON.parse(localStorage.getItem('currentUser')!));
    this.currentUser = this.currentUserSubject.asObservable();
    return this.currentUserSubject.value;
  }

  getHeaders(){
   return  new HttpHeaders({ 
      'Authorization': 'Bearer ' + String(this.currentUserValue.UserPassword)
     });
  }

  login(form: Credentials) {
    return this.http.post<any>(environment.apiUrl + '/userregistrationlogin/authenticate', form, { observe: 'response' })
      .pipe(map(res => {
        // login successful if there's a jwt token in the response
        if (res.body) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          res.body.UserPassword = 'password';
          localStorage.setItem('currentUser', JSON.stringify(res.body));
          this.cookieService.set('currentUser', JSON.stringify(res.body));
          this.currentUserSubject.next(res.body);
        }
        return res;
      }));
  }
  // login1(username: string, password: string) {
  //     return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password })
  //         .pipe(map(user => {
  //             // login successful if there's a jwt token in the response
  //             if (user && user.token) {
  //                 // store user details and jwt token in local storage to keep user logged in between page refreshes
  //                 localStorage.setItem('currentUser', JSON.stringify(user));
  //                 this.currentUserSubject.next(user);
  //             }

  //             return user;
  //         }));
  // }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.cookieService.deleteAll('currentUser');
    this.currentUserSubject.next(null!);
    this.router.navigate(['/AuthenticateUser/Login']);
  }
}
