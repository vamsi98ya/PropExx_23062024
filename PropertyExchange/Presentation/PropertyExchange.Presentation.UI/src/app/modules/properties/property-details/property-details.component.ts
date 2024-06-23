import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { throws } from 'assert/strict';
import { CookieService } from 'ngx-cookie-service';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { interval, Subscription } from 'rxjs';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { Property, TenantDetails } from 'src/app/shared/models/property.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { UserHolding, UserPassbook } from 'src/app/shared/models/userpassbook';
import { PercentageChangePipe } from 'src/app/shared/pipes/percentage-change.pipe';
import { AuthService } from 'src/app/shared/services/auth.service';
import { PropertyService } from 'src/app/shared/services/property.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrl: './property-details.component.css'
})
export class PropertyDetailsComponent {
  PropertyId: any;
  properties!: Property[];
  userHolding: UserHolding;
  submitted = false;
  selectedProperty: Property;
  popupVisible: boolean = false;
  buyorsellbtnclick: boolean = false
  orderForm: FormGroup;
  credentials: Credentials = new Credentials();
  doesUserDetailsExist: boolean;
  amountAvailableForInvesting: number;
  regModelForQuantity: RegExpModel = new RegExpModel();
  regExpressionForQuantity: RegExp = new RegExp(/^(?!0$)[0-9]+$/);

  regModelForPrice: RegExpModel = new RegExpModel();
  regExpressionForPrice: RegExp = new RegExp(/^(?!0$)[0-9]*(\.[0-9]{1,2})?$/);

  exceededBuyLimit: boolean = false;
  exceededSellLimit: boolean = false;
  buyOrderErrorMessage: string;
  sellOrderErrorMessage: string;
  doesUserHoldingExist: boolean;
  investedValue: number;
  currentValue: number;
  overallReturns: number;
  percentageReturn: number;
  orderExecuted: boolean;
  doesTenantDetailsExist: boolean;
  tenants!: TenantDetails[];
  isCustomPriceCheckboxChecked: boolean = false;
  priceLimitReached: boolean;
  minPrice: number = 0;
  maxPrice: number = 0;
  customPrice: number;
  customPriceReset: boolean = false;

  private refreshSubscription: Subscription;
  constructor(private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private cookieService: CookieService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private messageService: MessageService,
    private primengConfig: PrimeNGConfig) {
    this.regModelForQuantity.regex = this.regExpressionForQuantity;
    this.regModelForQuantity.errorMessage = 'Please provide a valid quantity in numbers';

    this.regModelForPrice.regex = this.regExpressionForPrice;
    this.regModelForPrice.errorMessage = 'Please provide a valid price';

    if (this.route.snapshot.paramMap.get('id') != null) {
      this.PropertyId = this.route.snapshot.paramMap.get('id');
    }
  }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.refreshData();
    this.refreshSubscription = interval(2000).subscribe(() => {
      this.refreshData();
    });
    if (this.selectedProperty != null && this.selectedProperty != undefined) {
      this.orderForm = this.formBuilder.group({
        CustomPriceCheckbox: [''],
        Price: ['', [Validators.required, Validators.pattern(this.regExpressionForPrice)]],
        Quantity: ['', [Validators.required, Validators.pattern(this.regExpressionForQuantity)]]
      });

      var user = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
      this.credentials.Email = user.UserEmail;
      this.credentials.PhoneNumber = user.UserPhoneNumber;
      this.credentials.Password = user.UserPassword;

      this.checkIfUserHoldingExist();
      this.checkIfUserDetailsExist();
      this.checkIfTenantDetailsExist();
    }
    else {
      this.router.navigate(['/PageNotFound']);
    }
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
    });
    this.properties = JSON.parse(window.localStorage.getItem('properties') || '{}');
    this.selectedProperty = this.properties.find(x => x.PropertyID == this.PropertyId) as Property;
    if (this.selectedProperty != null && this.selectedProperty != undefined) {
      this.selectedProperty.CreatedDate = formatDate(this.selectedProperty.CreatedDate, 'dd-MMM-yyyy', 'en-US');

      this.customPrice = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2));
      if (this.orderExecuted) {
        this.checkIfUserHoldingExist();
        this.checkIfUserDetailsExist();
      }
    }
  }

  get a() { return this.orderForm.controls; }

  ShowBuyOrSellPopup(buyorsell: boolean, propertyID: string, price: number) {
    if (this.authService.currentUserValue) {
      this.onReset();
      this.buyorsellbtnclick = buyorsell;
      this.exceededBuyLimit = false;
      this.exceededSellLimit = false;
      this.popupVisible = true;
      this.isCustomPriceCheckboxChecked = false;
    }
    else {
      window.alert("Please login to proceed further! \n\nProceeding to Login section...")
      this.router.navigate(['/AuthenticateUser/Login']);
    }
  }

  checkIfUserHoldingExist() {
    this.userService.getUserHolding(this.credentials.Email, this.credentials.PhoneNumber, this.selectedProperty.PropertyID)
      .subscribe(res => {
        if(res!== null){
          this.userHolding = res as UserHolding;
          this.doesUserHoldingExist = true;
          this.investedValue = this.userHolding.Quantity * Number(this.userHolding.Price.toFixed(2));
          this.currentValue = this.userHolding.Quantity * Number(this.selectedProperty.CurrentTokenPrice.toFixed(2));
          this.overallReturns = this.currentValue - this.investedValue;
          this.percentageReturn = this.overallReturns / this.investedValue
        }
        else{
            this.doesUserHoldingExist = false;
            this.investedValue = 0;
            this.currentValue = 0;
            this.overallReturns = this.currentValue - this.investedValue;
            this.percentageReturn = this.overallReturns / this.investedValue;
            this.doesUserHoldingExist = false;
        }
      },
        err => {
          if (err.status == 500) {
            this.doesUserHoldingExist = false;
            this.investedValue = 0;
            this.currentValue = 0;
            this.overallReturns = this.currentValue - this.investedValue;
            this.percentageReturn = this.overallReturns / this.investedValue;
          }
          if (err.status == 404) {
            this.doesUserHoldingExist = false;
          }
        });
  }

  checkIfUserDetailsExist() {
    this.userService.getUserDetails(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res != null){
        this.doesUserDetailsExist = true;
        this.amountAvailableForInvesting = res.AvailableMoneyForInvesting;
        }
        else{
          this.doesUserDetailsExist = false;
        }
      },
        err => {
          if (err.status == 404) {
            this.doesUserDetailsExist = false;
          }

        });
  }

  checkIfTenantDetailsExist() {
    this.propertyService.getTenantByPropertyID(this.PropertyId).subscribe(
      res => {

        this.tenants = res as TenantDetails[];
        if (this.tenants.length > 0) {
          this.doesTenantDetailsExist = true;
        }
        else {
          this.doesTenantDetailsExist = false;
        }


      },
      err => {
        this.doesTenantDetailsExist = false;
      }
    )
  }

  onCustomPriceCheckboxChanged(event: any) {
    this.isCustomPriceCheckboxChecked = event.srcElement.checked;
  }

  onOrderSubmit() {
    this.submitted = true;
    if (this.orderForm.invalid) {
      if (!this.isCustomPriceCheckboxChecked && this.orderForm.value.CustomPriceCheckbox === null && this.orderForm.value.Price === null) {

      }
      else {
        return;
      }
    }
    var orderform = this.orderForm.value;
    var user = new UserPassbook();

    if (this.buyorsellbtnclick) {
      if (Number(this.amountAvailableForInvesting.toFixed(2)) !== 0 && Number(this.amountAvailableForInvesting.toFixed(2)) >= (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * orderform.Quantity)) {
        if (Number(this.amountAvailableForInvesting.toFixed(2)) >= (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * orderform.Quantity)
          && orderform.Quantity <= this.selectedProperty.CurrentAvailableNumberOfTokens
          && this.selectedProperty.IsActive && this.selectedProperty.IsAvailableForInvesting
          && this.selectedProperty.CurrentTokenPrice != 0) {
          this.exceededBuyLimit = false;
          user.UserEmail = this.credentials.Email;
          user.UserPhoneNumber = this.credentials.PhoneNumber;
          user.PropertyID = this.selectedProperty.PropertyID;
          user.TxnNumber = Math.floor(1000000000 + Math.random() * 9000000000).toString();
          user.TxnType = 'BUY';
          user.Quantity = orderform.Quantity;
          if (!this.isCustomPriceCheckboxChecked) {
            user.Price = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2));
          }
          else {
            if (!this.priceLimitReached) {
              user.Price = Number(orderform.Price.toFixed(2));
            }
            else {
              return;
            }
          }
          user.TotalTxnValue = user.Price * orderform.Quantity;
          user.OrderDateTime = this.formatDate(new Date());
          user.IsCurrentHolding = true;
          this.userService.postUserOrderDetails(user)
            .subscribe(res => {
              // this.checkIfUserDetailsExist();
              // this.checkIfUserHoldingExist();
              this.orderExecuted = true;
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Congratulations! Your buy order of value ' + user.TotalTxnValue.toString() + '/- executed successfully.' });
            },
              err => {
                if (err.status == 404) {
                  this.doesUserDetailsExist = false;
                }

              });
        }
        else {
          this.exceededBuyLimit = true;
        }
        this.onReset();
        this.exceededBuyLimit = false;
        this.exceededSellLimit = false;
        this.popupVisible = false;
        this.isCustomPriceCheckboxChecked = false;
        this.buyOrderErrorMessage = '';
        this.sellOrderErrorMessage = '';
        this.router.navigate(['/Properties/' + this.selectedProperty.PropertyID.toString()]);
      }
      else if (orderform.Quantity > this.selectedProperty.CurrentAvailableNumberOfTokens) {

      }
    }
    else {
      //To be implemented
      if (this.investedValue !== 0) {
        if (orderform.Quantity < this.userHolding.Quantity && this.selectedProperty.IsActive
          && (orderform.Quantity + this.selectedProperty.CurrentAvailableNumberOfTokens) <= this.selectedProperty.CurrentTotalNumberOfTokens) {
          this.exceededSellLimit = false;
          user.UserEmail = this.credentials.Email;
          user.UserPhoneNumber = this.credentials.PhoneNumber;
          user.PropertyID = this.selectedProperty.PropertyID;
          user.TxnNumber = Math.floor(1000000000 + Math.random() * 9000000000).toString();
          user.TxnType = 'SELL';
          user.Quantity = orderform.Quantity;
          if (!this.isCustomPriceCheckboxChecked) {
            user.Price = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2));
          }
          else {
            if (!this.priceLimitReached) {
              user.Price = Number(orderform.Price.toFixed(2));
            }
            else {
              return;
            }
          }
          user.TotalTxnValue = user.Price * orderform.Quantity;
          user.OrderDateTime = this.formatDate(new Date());
          user.IsCurrentHolding = true;
          this.userService.postUserOrderDetails(user)
            .subscribe(res => {
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Congratulations! Your sell order of value ' + user.TotalTxnValue.toString() + '/- executed successfully.' });
              this.checkIfUserDetailsExist();
              this.checkIfUserHoldingExist();
            },
              err => {
                if (err.status == 404) {
                  this.doesUserDetailsExist = false;
                }

              });
        }
        else if (orderform.Quantity == this.userHolding.Quantity && this.selectedProperty.IsActive
          && (orderform.Quantity + this.selectedProperty.CurrentAvailableNumberOfTokens) <= this.selectedProperty.CurrentTotalNumberOfTokens) {
          this.exceededSellLimit = false;
          user.UserEmail = this.credentials.Email;
          user.UserPhoneNumber = this.credentials.PhoneNumber;
          user.PropertyID = this.selectedProperty.PropertyID;
          user.TxnNumber = Math.floor(1000000000 + Math.random() * 9000000000).toString();
          user.TxnType = 'SELL';
          user.Quantity = orderform.Quantity;
          if (!this.isCustomPriceCheckboxChecked) {
            user.Price = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2));
          }
          else {
            if (!this.priceLimitReached) {
              user.Price = Number(orderform.Price.toFixed(2));
            }
            else {
              return;
            }
          }
          user.TotalTxnValue = user.Price * orderform.Quantity;
          user.OrderDateTime = this.formatDate(new Date());
          user.IsCurrentHolding = false;
          this.userService.postUserOrderDetails(user)
            .subscribe(res => {
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Congratulations! Your sell order of value ' + user.TotalTxnValue.toString() + '/- executed successfully.' });
              this.checkIfUserDetailsExist();
              this.checkIfUserHoldingExist();
            },
              err => {
                if (err.status == 404) {
                  this.doesUserDetailsExist = false;
                }
              });
        }
        else {
          this.exceededSellLimit = true;
        }
        this.onReset();
        this.exceededBuyLimit = false;
        this.exceededSellLimit = false;
        this.popupVisible = false;
        this.isCustomPriceCheckboxChecked = false;
        this.buyOrderErrorMessage = '';
        this.sellOrderErrorMessage = '';
        this.router.navigate(['/Properties/' + this.selectedProperty.PropertyID.toString()]);
      }
      else {
        this.exceededSellLimit = true;
        this.sellOrderErrorMessage = 'Exceeded Quantity'
      }
    }
  }

  handlePriceChange(event: any) {
    if (event !== null) {
      this.orderForm.value.Price = event;
    }
    //Handles Buy Quantity field changes
    if ((this.orderForm.value.Price != '' || this.orderForm.value.Price != undefined || this.orderForm.value.Price != null)
      && Number(this.orderForm.value.Price) > 0) {
      if (this.buyorsellbtnclick) {
        if (Number(this.orderForm.value.Price.toFixed(2)) > (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 1.2)) {
          this.priceLimitReached = true;
          this.maxPrice = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 1.2;
          this.maxPrice = Number(this.maxPrice.toFixed(2));
          this.minPrice = 0;
        }
        else if (Number(this.orderForm.value.Price.toFixed(2)) < (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 0.8)) {
          this.priceLimitReached = true;
          this.minPrice = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 0.8;
          this.minPrice = Number(this.minPrice.toFixed(2));
          this.maxPrice = 0;
        }
        else {
          this.priceLimitReached = false;
          this.minPrice = 0;
          this.maxPrice = 0;
        }
      }
    }

    //Handles Sell Quantity field changes
    if ((this.orderForm.value.Price != '' || this.orderForm.value.Price != undefined || this.orderForm.value.Price != null)
      && Number(this.orderForm.value.Price) > 0) {
      if (!this.buyorsellbtnclick) {
        if (Number(this.orderForm.value.Price.toFixed(2)) > (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 1.2)) {
          this.priceLimitReached = true;
          this.maxPrice = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 1.2;
          this.maxPrice = Number(this.maxPrice.toFixed(2));
          this.minPrice = 0;
        }
        else if (Number(this.orderForm.value.Price.toFixed(2)) < (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 0.8)) {
          this.priceLimitReached = true;
          this.minPrice = Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * 0.8;
          this.minPrice = Number(this.minPrice.toFixed(2));
          this.maxPrice = 0;
        }
        else {
          this.priceLimitReached = false;
          this.minPrice = 0;
          this.maxPrice = 0;
        }
      }
    }
  }

  handleQuantityChange(event: any) {
    //Handles Buy Quantity field changes
    if (this.orderForm.value.Quantity != '' || this.orderForm.value.Quantity != undefined || this.orderForm.value.Quantity != null) {
      if (this.buyorsellbtnclick) {
        if (Number(this.amountAvailableForInvesting.toFixed(2)) >= (Number(this.selectedProperty.CurrentTokenPrice.toFixed(2)) * this.orderForm.value.Quantity)
          && this.orderForm.value.Quantity <= this.selectedProperty.CurrentAvailableNumberOfTokens) {
          this.exceededBuyLimit = false;
        }
        else if (this.orderForm.value.Quantity > this.selectedProperty.CurrentAvailableNumberOfTokens) {
          this.buyOrderErrorMessage = 'Exceeded Quantity';
          this.exceededBuyLimit = true;
        }
        else {
          if (Number.isInteger(this.orderForm.value.Quantity)) {
            this.buyOrderErrorMessage = 'Insuffiecient Funds'
            this.exceededBuyLimit = true;
          }
        }
      }
    }

    //Handles Sell Quantity field changes
    if (this.orderForm.value.Quantity != '' || this.orderForm.value.Quantity != undefined || this.orderForm.value.Quantity != null) {
      if (!this.buyorsellbtnclick) {
        //Verify our holdings value is >= (current token price * the entered quantity)
        if (this.doesUserHoldingExist && this.orderForm.value.Quantity <= this.userHolding.Quantity) {
          this.exceededSellLimit = false;
        }
        else if (!this.doesUserHoldingExist) {
          this.exceededSellLimit = false;
        }
        else {
          this.sellOrderErrorMessage = 'Exceeded Quantity';
          this.exceededSellLimit = true;
        }

      }
    }
  }

  onReset() {
    this.submitted = false;
    this.orderForm.reset();
  }

  formatDate(date: Date): string {
    const month = ('0' + (date.getMonth() + 1)).slice(-2); // Get month (0-indexed)
    const day = ('0' + date.getDate()).slice(-2); // Get day
    const year = date.getFullYear(); // Get full year
    const hours = ('0' + date.getHours()).slice(-2); // Get hours
    const minutes = ('0' + date.getMinutes()).slice(-2); // Get minutes
    const seconds = ('0' + date.getSeconds()).slice(-2); // Get seconds

    // Construct the formatted date string
    return `${month}/${day}/${year} ${hours}:${minutes}:${seconds}`;
  }

}

