import { Component, OnInit } from '@angular/core';
import { Property } from 'src/app/shared/models/property.model';
import { PropertyService } from 'src/app/shared/services/property.service';

@Component({
  selector: 'app-properties',
  templateUrl: './properties.component.html',
  styleUrls: ['./properties.component.css']
})
export class PropertiesComponent implements OnInit {

properties!: Property[];

  constructor(public propertyService:PropertyService) { }

  ngOnInit(): void {
  //   this.propertyService.getProperties().subscribe(res =>{
  //     this.properties = res as Property[];
  //     if(window.localStorage.getItem('properties') != null){
  //       window.localStorage.removeItem('properties');
  //     }
  //     window.localStorage.setItem('properties', JSON.stringify(res));
  // }
  //   )
  }

}
