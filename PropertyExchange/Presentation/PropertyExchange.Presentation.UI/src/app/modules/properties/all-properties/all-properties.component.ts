import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Table } from 'primeng/table';
import { Property } from 'src/app/shared/models/property.model';
import { PropertyService } from 'src/app/shared/services/property.service';
import { interval, Subscription } from 'rxjs';
import {UserDetailsComponent} from '../../user-registration-login/user-details/user-details.component';

@Component({
  selector: 'app-all-properties',
  templateUrl: './all-properties.component.html',
  styleUrls: ['./all-properties.component.css']
})
export class AllPropertiesComponent implements OnInit {

  properties!: Property[];
  inputValue: string = '';
  private refreshSubscription: Subscription;
  constructor(public propertyService: PropertyService,
    private router: Router,
    private cookieService: CookieService) { }

  ngOnInit(): void {
    this.refreshData();
    // this.properties = JSON.parse(window.localStorage.getItem('properties') || '{}');
    

    this.refreshSubscription = interval(2000).subscribe(() => {
      this.refreshData();
    });
  }

  ngOnDestroy(): void {
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }

  refreshData() {

    this.propertyService.getProperties().subscribe(res => {
      this.properties = res as Property[];
      if (window.localStorage.getItem('properties') != null) {
        window.localStorage.removeItem('properties');
      }
      window.localStorage.setItem('properties', JSON.stringify(res));
    }
    )
  }

  clear(dt1: Table) {
    this.inputValue = '';
    dt1.clear();

  }

  filter(dt1: Table) {
    dt1.filterGlobal(this.inputValue, 'contains');
  }

  onPropertyRowClick(PropertyID: string) {
    this.router.navigate(['/Properties/', PropertyID]);

    if (window.localStorage.getItem('selectedProperty') != null) {
      window.localStorage.removeItem('selectedProperty');
    }
    window.localStorage.setItem('selectedProperty', JSON.stringify(PropertyID));

    //window.open(window.location.origin + "/Properties/" + PropertyID);
  }

  getSeverity(status: string) {
    switch (status) {
      case 'true':
        return 'success';
      case 'false':
        return 'danger';
    }
  }
  visible: boolean = false;
  showDialog() {
    this.visible = true;
}

}
