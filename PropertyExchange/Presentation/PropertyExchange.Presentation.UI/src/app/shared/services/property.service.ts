import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Property } from '../models/property.model';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  
  readonly apiUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getProperties(){
    return this.http.get(this.apiUrl + "/property/getallproperties");
  }

  getPropertyByID(propertyID: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('propertyID', propertyID)

    // Make GET request with parameters
    return this.http.get<any>(this.apiUrl+'/property/' + propertyID);
  }

  getTenantByPropertyID(propertyID: string) {
    // Create HttpParams object and set parameters
    let params = new HttpParams()
      .set('propertyID', propertyID)

    // Make GET request with parameters
    return this.http.get<any>(this.apiUrl+'/tenant/' + propertyID);
  }
}
