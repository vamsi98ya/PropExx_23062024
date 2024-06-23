import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { UserRegistrationLogin } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { MustMatch } from '../_helpers/must-match.validator';
import { RegistrationService } from './registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  regModelforEmail: RegExpModel = new RegExpModel();
  regModelforPhoneNumber: RegExpModel = new RegExpModel();
  regModelforPassword: RegExpModel = new RegExpModel();

  regExpressionForEmail: RegExp = new RegExp(/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/);
  regExpressionForPhoneNumber: RegExp = new RegExp(/^\d{10}$/);
  regExpressionForPassword: RegExp = new RegExp(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/);

  constructor(
    private router: Router,
    private formBuilder: FormBuilder, 
    private regService: RegistrationService,
    private authService: AuthService,
    private messageService: MessageService, 
    private primengConfig: PrimeNGConfig) {
    this.regModelforEmail.regex = this.regExpressionForEmail;
    this.regModelforEmail.errorMessage = 'Please provide a valid email address';

    this.regModelforPhoneNumber.regex = this.regExpressionForPhoneNumber;
    this.regModelforPhoneNumber.errorMessage = 'Please provide a valid phone number';

    this.regModelforPassword.regex = this.regExpressionForPassword;
    this.regModelforPassword.errorMessage = 'Please provide a valid password';
     }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    if(this.authService.currentUserValue){
      this.authService.logout();
    }
    this.registerForm = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email, Validators.pattern(this.regExpressionForEmail)]],
      PhoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.pattern(this.regExpressionForPhoneNumber)]],
      Password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(this.regExpressionForPassword)]],
      ConfirmPassword: ['', Validators.required],
    },
      {
        validator: MustMatch('Password', 'ConfirmPassword')
      });
  }
  get f() { return this.registerForm.controls; }
  onSubmit() {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    var userData = this.registerForm.value;
    var user = new UserRegistrationLogin();
    user.UserEmail = userData.Email;
    user.UserPhoneNumber = userData.PhoneNumber as string;
    user.UserPassword = userData.Password;
    if (user.UserEmail.match(this.regModelforEmail.regex) 
    && user.UserPhoneNumber.match(this.regModelforPhoneNumber.regex)
    && user.UserPassword.match(this.regModelforPassword.regex)) {
      this.regService.postUser(user)
        .subscribe(res => {
          if (res.status == 201) {
            this.messageService.add({severity:'success', summary: 'Success', detail: 'User registered successfully! \n\nProceeding to Login section...'});
            setTimeout(() => {
              this.router.navigate(['/AuthenticateUser/Login']);
          }, 4000);
          }
          
        },
          err => {
            if (err.status == 500) {
              //alert("A user already exists with the given email or phone number");
            this.messageService.add({severity:'error', summary: 'Error', detail: 'A user already exists with the given email or phone number'});
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

  onReset(){
    this.submitted = false;
    this.registerForm.reset();
  }

}

