import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { Property } from 'src/app/shared/models/property.model';
import { UserHolding } from 'src/app/shared/models/userpassbook';
import { UserDetails, UserRegistrationLogin } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { PropertyService } from 'src/app/shared/services/property.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-welcome-investor',
  templateUrl: './welcome-investor.component.html',
  styleUrls: ['./welcome-investor.component.css']
})
export class WelcomeInvestorComponent implements OnInit {

  constructor(public authService: AuthService,
    public userService: UserService,
    private propertyService: PropertyService,
    private router:Router) { }
  properties!: Property[];
  userDetails: UserDetails;
  userHoldings!: UserHolding[];
  myInvestments: MyInvestments[] = [];
  // myInvestment: MyInvestments = new MyInvestments();
  credentials: Credentials = new Credentials();
  investmentSummary: InvestmentSummary = new InvestmentSummary();
  doesUserDetailsExist: boolean = true;
  holdingsExist: boolean;
  data: any;
  private refreshSubscription: Subscription;
  chartOptions: any;
  subscription: Subscription;
  labels: string[] = [];
  currentInvestmentValue: number[] = [];

   ngOnInit(): void {
    this.getAllProperties();
    this.refreshSubscription = interval(2000).subscribe(() => {
       this.propertyService.getProperties().subscribe(res => {
        this.properties = res as Property[];
  
        if (window.localStorage.getItem('properties') != null) {
          window.localStorage.removeItem('properties');
        }
        window.localStorage.setItem('properties', JSON.stringify(res));
      this.properties = JSON.parse(window.localStorage.getItem('properties') || '{}');
      
      });
    });
    
  }

   getAllProperties() {
     this.propertyService.getProperties().subscribe(res => {
      this.properties = res as Property[];

      if (window.localStorage.getItem('properties') != null) {
        window.localStorage.removeItem('properties');
      }
      window.localStorage.setItem('properties', JSON.stringify(res));
    this.properties = JSON.parse(window.localStorage.getItem('properties') || '{}');
    var user = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
    this.credentials.Email = user.UserEmail;
    this.credentials.PhoneNumber = user.UserPhoneNumber;
    this.checkIfUserDetailsExist();
    this.getAllUserHoldings();
    this.displayChartData();
    });

  }

  getAllUserHoldings() {
    this.userService.getAllUserHoldings(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res.length > 0){
        this.userHoldings = res as UserHolding[];
        this.investmentSummary.TotalInvestment = this.userHoldings.reduce((acc, obj) => acc + (obj.Price * obj.Quantity), 0);
        this.investmentSummary.CurrentValue = 0;
        for (var i = 0; i < this.userHoldings.length; i++) {
          var selectedProperty = this.properties.find(x => x.PropertyID == this.userHoldings[i].PropertyID) as Property;
          this.investmentSummary.CurrentValue = this.investmentSummary.CurrentValue + (Number(selectedProperty.CurrentTokenPrice.toFixed(2)) * this.userHoldings[i].Quantity);
          var myInvestment = new MyInvestments();
          myInvestment.PropertyID = this.userHoldings[i].PropertyID;
          myInvestment.PropertyName = this.userHoldings[i].PropertyName;
          myInvestment.Quantity = this.userHoldings[i].Quantity;
          myInvestment.Price = this.userHoldings[i].Price;
          myInvestment.InvestmentValue = myInvestment.Quantity * myInvestment.Price;
          myInvestment.CurrentPrice = selectedProperty.CurrentTokenPrice;
          myInvestment.CurrentValue = myInvestment.Quantity * myInvestment.CurrentPrice;
          myInvestment.TotalReturns = myInvestment.CurrentValue - myInvestment.InvestmentValue;
          myInvestment.ReturnsPercentage = (myInvestment.CurrentValue - myInvestment.InvestmentValue) / myInvestment.InvestmentValue;
          this.myInvestments.push(myInvestment);
          this.labels.push(this.userHoldings[i].PropertyName)
          this.currentInvestmentValue.push(Number(myInvestment.CurrentValue.toFixed(2)));
        }

        this.investmentSummary.TotalReturns = this.investmentSummary.CurrentValue - this.investmentSummary.TotalInvestment;
        this.investmentSummary.ReturnPercentage = (this.investmentSummary.CurrentValue - this.investmentSummary.TotalInvestment) / this.investmentSummary.TotalInvestment;
        this.holdingsExist = true;
      }
      else{
        this.holdingsExist = false;
      }
      },
        err => {
          this.holdingsExist = false;
        });
  }

  checkIfUserDetailsExist() {
    this.userService.getUserDetails(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res !== null){
        this.doesUserDetailsExist = true;
        }
        else{
          this.doesUserDetailsExist = false;
        }
      },
        err => {
          if (err.status == 404) {
            this.doesUserDetailsExist = false;
            //this.toastr.warning(err.error, 'Registerd Failed')
          }
        });
  }

  onPropertyRowClick(PropertyID: string) {
    this.router.navigate(['/Properties/', PropertyID]);

    if (window.localStorage.getItem('selectedProperty') != null) {
      window.localStorage.removeItem('selectedProperty');
    }
    window.localStorage.setItem('selectedProperty', JSON.stringify(PropertyID));

    //window.open(window.location.origin + "/Properties/" + PropertyID);
  }

  displayChartData() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');
    if (this.myInvestments != null) {
      this.data = {
        labels: this.labels,
        datasets: [
          {
            data: this.currentInvestmentValue
          }
        ]
      };
    }

    this.chartOptions = this.getLightTheme();
  }

  getLightTheme() {
    return {
      plugins: {
        legend: {
          labels: {
            color: '#495057'
          }
        }
      }
    }
  }

  ngOnDestroy(): void {
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }
}

class InvestmentSummary {
  TotalInvestment: number;
  CurrentValue: number;
  TotalReturns: number;
  ReturnPercentage: number;
}


class MyInvestments {
  PropertyID: string;
  PropertyName: string;
  Quantity: number;
  Price: number;
  InvestmentValue: number;
  CurrentPrice: number;
  CurrentValue: number;
  TotalReturns: number;
  ReturnsPercentage: number;
  IsActive: boolean;
}