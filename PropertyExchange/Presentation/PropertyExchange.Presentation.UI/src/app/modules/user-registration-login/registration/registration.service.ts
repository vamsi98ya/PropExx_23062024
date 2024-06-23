import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRegistrationLogin } from 'src/app/shared/models/userregistrationlogin.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  readonly apiUri = environment.apiUrl ;

  constructor(private http:HttpClient) { }

  postUser(user:UserRegistrationLogin){
    return this.http.post<any>(this.apiUri+'/userregistrationlogin/postuser', user, {observe:'response'});
  }
}
