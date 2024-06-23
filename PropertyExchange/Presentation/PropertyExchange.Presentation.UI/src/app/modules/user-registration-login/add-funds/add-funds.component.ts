import { DecimalPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { Property } from 'src/app/shared/models/property.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { UserFunds } from 'src/app/shared/models/userfunds';
import { UserPassbook } from 'src/app/shared/models/userpassbook';
import { UserDetails } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-add-funds',
  templateUrl: './add-funds.component.html',
  styleUrl: './add-funds.component.css'
})
export class AddFundsComponent {
  fundsForm: FormGroup;
  //withdrawFundsForm: FormGroup;
  submitted = false;
  regModelForMoney: RegExpModel = new RegExpModel();
  regModelforEmail: RegExpModel = new RegExpModel();
  regModelforPhoneNumber: RegExpModel = new RegExpModel();
  doesUserDetailsExist: boolean = true;
  credentials: Credentials = new Credentials();
  AvailableForInvesting: number;
  amountAvailableForInvesting: number;
  exceededWithdrawAmount:boolean;
  popupVisible: boolean = false;
  ledgerPopupVisible: boolean = false;
  userFunds!: UserFunds[];
  userPassbook!:UserPassbook[];
isUserFundsHistoryAvailable:boolean;
isUserLedgerHistoryAvailable:boolean;
properties!: Property[];
moneyAdded: number;

  regExpressionForMoney: RegExp = new RegExp(/^\d+(\.\d{1,2})?$/);//new RegExp(/^[0-9]+$/);
  regExpressionForEmail: RegExp = new RegExp(/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/);
  regExpressionForPhoneNumber: RegExp = new RegExp(/^\d{10}$/);

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private authService: AuthService,
    private messageService: MessageService, 
    private primengConfig: PrimeNGConfig) {
    this.regModelForMoney.regex = this.regExpressionForMoney;
    this.regModelForMoney.errorMessage = 'Please provide a valid amount in numbers';

    this.regModelforEmail.regex = this.regExpressionForEmail;
    this.regModelforEmail.errorMessage = 'Please provide a valid email address';

    this.regModelforPhoneNumber.regex = this.regExpressionForPhoneNumber;
    this.regModelforPhoneNumber.errorMessage = 'Please provide a valid phone number';
  }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.userService.refresh$.subscribe(() => {
      if (this.authService.currentUserValue) {
        this.fundsForm = this.formBuilder.group({
          Money: ['', [Validators.required, Validators.pattern(this.regExpressionForMoney)]],
        });

        // this.withdrawFundsForm = this.formBuilder.group({
        //   WithdrawMoney: ['', [Validators.required, Validators.pattern(this.regExpressionForMoney)]]
        // });
      }

      var user = JSON.parse(window.localStorage.getItem('currentUser') || '{}');

      this.credentials.Email = user.UserEmail;
      this.credentials.PhoneNumber = user.UserPhoneNumber;
      this.credentials.Password = user.UserPassword;
      this.checkIfUserDetailsExist()
      this.properties = JSON.parse(window.localStorage.getItem('properties') || '{}');
    });
  }

  checkIfUserDetailsExist() {
    this.userService.getUserDetails(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res !== null){
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

  get a() { return this.fundsForm.controls; }

  //get w() {return this.withdrawFundsForm.controls}

  onSubmit(action: string) {
    this.submitted = true;
    if (this.fundsForm.invalid) {
      return;
    }
    const clickedButtonId = action;
    var userData = this.fundsForm.value;
    var user = new UserFunds();
    if (clickedButtonId === 'ADD') {
      user.AddOrWithdraw = "ADD";
      user.Description = "Money added by the user";
      this.exceededWithdrawAmount = false;
      if(userData.Money == '0'){
        //this.exceededWithdrawAmount = true;
        this.onReset();
        return
      }
    }
    else {
      user.AddOrWithdraw = "WITHDRAW";
      user.Description = "Money withdrawn by the user";
      if(userData.Money.toFixed(2) > 0 && Number(userData.Money) > Number(this.amountAvailableForInvesting.toFixed(2))){
        this.exceededWithdrawAmount = true;
        this.onReset();
        return
      }
      else if(userData.Money.toFixed(2) == '0'){
        //this.exceededWithdrawAmount = true;
        this.onReset();
        return
      }
      else{
        this.exceededWithdrawAmount = false;
      }
    }
    user.TxnNumber = Math.floor(1000000000 + Math.random() * 9000000000).toString();
    user.TxnAmount = userData.Money.toFixed(2);
    user.ModeOfTxn = "Net Banking"; //userData.ModeOfTxn;
    user.TxnDateTime = this.formatDate(new Date()); //this.datePipe.transform(new Date()., 'MM/dd/yyyy HH:mm:ss') as string;
    user.BankName = "State Bank Of India"
    user.AccountNumber = "10000000001"
    
    var currentUser = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
    user.UserEmail = currentUser.UserEmail;
    user.UserPhoneNumber = currentUser.UserPhoneNumber;

    if (user.TxnNumber.length > 0 && user.TxnAmount != 0 && user.ModeOfTxn.length > 0 && user.TxnDateTime.length > 0) {
      this.userService.addUserFunds(user)
        .subscribe(res => {
          if (res.status == 201) {
            if(user.AddOrWithdraw === 'ADD'){
            this.messageService.add({severity:'success', summary: 'Success', detail: 'Funds worth Rs.' + user.TxnAmount + '/- added successfully!'});
            }
            else{
            this.messageService.add({severity:'success', summary: 'Success', detail: 'Funds worth Rs.' + user.TxnAmount + '/- withdrawn successfully!'});
            }
            this.checkIfUserDetailsExist()
            this.onReset();
            //alert("User details saved successfully!");
            //this.toastr.success(res.body.url, 'Registerd Successfully')
            //this.router.navigate(['/AuthenticateUser/Login']);
          }

        },
          err => {
            if (err.status == 500) {
              alert("Internal server error! Please try after some time");
              //this.toastr.warning(err.error, 'Registerd Failed')
            }
            else if (err.status == 400) {

            }
            else if (err.status == 200) {
              alert(err.error.text);
            }
          });
    }
    else {
      //this.toastr.warning(this.regModel.errorMessage);
    }
  }

  onAddFundsButtonClick(amount: number){
    if(this.moneyAdded == undefined){
      this.moneyAdded = 0;
    }
    this.moneyAdded += amount;
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

  onReset() {
    this.submitted = false;
    this.fundsForm.reset();
  }

  showFundsHistory() {
    if(this.authService.currentUserValue){
      this.userService.getUserFundsHistory(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res !== null){

        
        this.userFunds = res;
        this.isUserFundsHistoryAvailable = true;
        }
        else{
          this.isUserFundsHistoryAvailable = false;
        }
      },
        err => {
          if (err.status == 404) {
            this.isUserFundsHistoryAvailable = false;
          }
        });
      this.popupVisible = true;
    }
    else{
      window.alert("Please login to proceed further! \n\nProceeding to Login section...")
    }
  }

  showLedgerHistory(){
    if(this.authService.currentUserValue){
      this.userService.getLedgerHistory(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res !== null){
        this.userPassbook = res;
        for (var i = 0; i < this.userPassbook.length; i++) {
          var selectedProperty = this.properties.find(x => x.PropertyID == this.userPassbook[i].PropertyID) as Property;
          this.userPassbook[i].PropertyID = selectedProperty.Name;
        }
        this.isUserLedgerHistoryAvailable = true;
      }
      else{
        this.isUserLedgerHistoryAvailable = false;
      }
      },
        err => {
          if (err.status == 404) {
            this.isUserLedgerHistoryAvailable = false;
          }

        });
      this.ledgerPopupVisible = true;
    }
    else{
      window.alert("Please login to proceed further! \n\nProceeding to Login section...")
    }
  }
}
