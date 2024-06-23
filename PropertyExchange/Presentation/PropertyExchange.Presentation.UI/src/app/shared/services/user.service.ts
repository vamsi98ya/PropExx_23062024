import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Credentials } from '../models/credentials.model';
import { UserFunds } from '../models/userfunds';
import { UserPassbook } from '../models/userpassbook';
import { ChangePassword, UserDetails } from '../models/userregistrationlogin.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  readonly apiUri = environment.apiUrl ;

  constructor(private http:HttpClient) { }

  postUserDetails(user: UserDetails){
    return this.http.post<any>(this.apiUri+'/user/postuserdetails', user, {observe:'response'});
  }

  // getUserDetails(credentials: Credentials){
  //   return this.http.post<any>(this.apiUri+'/user/getuserdetails', credentials, {observe:'response'});
  // }

  getUserDetails(email: string, phoneNumber: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('email', email)
      .set('phoneNumber', phoneNumber);

    // Make GET request with parameters
    return this.http.get<any>(this.apiUri+'/user/getuserdetails', { params });
  }

  postChangePassword(changePassword: ChangePassword) {
    return this.http.post<any>(this.apiUri+'/userregistrationlogin/changepassword', changePassword, {observe:'response'});
  }

  addUserFunds(userFunds: UserFunds) {
    return this.http.post<any>(this.apiUri+'/user/postuserfunddetails', userFunds, {observe:'response'});
  }

  // getUserFundsHistory(credentials: Credentials){
  //   return this.http.post<any>(this.apiUri+'/user/getuserfundshistory', credentials, {observe:'response'});
  // }

  getUserFundsHistory(email: string, phoneNumber: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('email', email)
      .set('phoneNumber', phoneNumber);

    // Make GET request with parameters
    return this.http.get<any>(this.apiUri+'/user/getuserfundshistory', { params });
  }

  getLedgerHistory(email: string, phoneNumber: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('email', email)
      .set('phoneNumber', phoneNumber);

    // Make GET request with parameters
    return this.http.get<any>(this.apiUri+'/user/getledgerhistory', { params });
  }

  postUserOrderDetails(order: UserPassbook){
    return this.http.post<any>(this.apiUri+'/user/postuserorderdetails', order, {observe:'response'});
  }

  // getUserHolding(credentials: Credentials){
  //   return this.http.post<any>(this.apiUri+'/user/getuserholding', credentials, {observe:'response'});
  // }

  getUserHolding(email: string, phoneNumber: string, propertyID: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('email', email)
      .set('phoneNumber', phoneNumber)
      .set('propertyID', propertyID);

    // Make GET request with parameters
    return this.http.get<any>(this.apiUri+'/user/getuserholding', { params })
    .pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (err.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${err.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${err.status}\nMessage: ${err.message}`;
    }
    return throwError(errorMessage);
  }

  // getAllUserHoldings(credentials: Credentials){
  //   return this.http.post<any>(this.apiUri+'/user/getuserportfolio', credentials, {observe:'response'});
  // }

  getAllUserHoldings(email: string, phoneNumber: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('email', email)
      .set('phoneNumber', phoneNumber);

    // Make GET request with parameters
    return this.http.get<any>(this.apiUri+'/user/getuserportfolio', { params });
  }

  private refreshSubject = new BehaviorSubject<boolean>(true);

  // Observable to watch for refresh events
  refresh$ = this.refreshSubject.asObservable();

  // Method to trigger refresh
  triggerRefresh() {
    this.refreshSubject.next(true);
  }
}
