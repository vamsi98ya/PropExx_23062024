import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { UserDetails } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-personal-details',
  templateUrl: './personal-details.component.html',
  styleUrl: './personal-details.component.css'
})
export class PersonalDetailsComponent {
  userDetailsForm: FormGroup;
  submitted = false;
  maxDate: string = new Date().toISOString().split('T')[0];
  regModelforEmail: RegExpModel = new RegExpModel();
  regModelforPhoneNumber: RegExpModel = new RegExpModel();
  doesUserDetailsExist: boolean = true;
  credentials: Credentials = new Credentials();

  regExpressionForEmail: RegExp = new RegExp(/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/);
  regExpressionForPhoneNumber: RegExp = new RegExp(/^\d{10}$/);

  constructor(
    private formBuilder: FormBuilder, 
    private userService: UserService,
    private authService: AuthService,
    private messageService: MessageService, 
    private primengConfig: PrimeNGConfig) {
    this.regModelforEmail.regex = this.regExpressionForEmail;
    this.regModelforEmail.errorMessage = 'Please provide a valid email address';

    this.regModelforPhoneNumber.regex = this.regExpressionForPhoneNumber;
    this.regModelforPhoneNumber.errorMessage = 'Please provide a valid phone number';
     }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    if(this.authService.currentUserValue){
    this.userDetailsForm = this.formBuilder.group({
      Name: ['', Validators.required],
      DOB: ['', Validators.required],
      Gender: ['', Validators.required],
      SecondaryEmail: ['', [Validators.email, Validators.pattern(this.regExpressionForEmail)]],
      SecondaryPhoneNumber: ['', [Validators.minLength(10), Validators.pattern(this.regExpressionForPhoneNumber)]],
      Occupation: ['', Validators.required],
      IncomeRange: ['', Validators.required],
      MaritalStatus: ['', Validators.required],
      FatherName: ['', Validators.required]
    });
  }

  var user = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
   
  this.credentials.Email = user.UserEmail;
  this.credentials.PhoneNumber = user.UserPhoneNumber;
  this.credentials.Password = user.UserPassword;
  this.checkIfUserDetailsExist()
  }

  checkIfUserDetailsExist() {
    this.userService.getUserDetails(this.credentials.Email, this.credentials.PhoneNumber)
      .subscribe(res => {
        if(res !== null){
        this.doesUserDetailsExist = true;
        this.userDetailsForm.patchValue(res);
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

  get f() { return this.userDetailsForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.userDetailsForm.invalid) {
      return;
    }
    var userData = this.userDetailsForm.value;
    var user = new UserDetails();
    user.Name = userData.Name;
    user.DOB = userData.DOB;
    user.Gender = userData.Gender;
    user.SecondaryEmail = userData.SecondaryEmail;
    user.SecondaryPhoneNumber = userData.SecondaryPhoneNumber as string;
    user.Occupation = userData.Occupation;
    user.IncomeRange = userData.IncomeRange;
    user.MaritalStatus = userData.MaritalStatus;
    user.FatherName = userData.FatherName;

    var currentUser = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
    user.UserEmail = currentUser.UserEmail;
    user.UserPhoneNumber = currentUser.UserPhoneNumber;

    if (user.Name.length > 0 && user.DOB.length > 0 && user.Gender.length > 0) {
      this.userService.postUserDetails(user)
        .subscribe(res => {
          if (res.status == 201) {
            this.messageService.add({severity:'success', summary: 'Success', detail: 'Details saved successfully.'});
            this.checkIfUserDetailsExist()
            this.triggerRefreshOnAddFundsComponent()
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
            else if(err.status == 400){

            }
            else if(err.status == 200){
              alert(err.error.text);
            }
          });
    }
    else {
      //this.toastr.warning(this.regModel.errorMessage);
    }
  }

  triggerRefreshOnAddFundsComponent() {
    this.userService.triggerRefresh();
  }

  onReset(){
    this.submitted = false;
    this.userDetailsForm.reset();
  }

}
