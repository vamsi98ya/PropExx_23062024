import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { ChangePassword, UserRegistrationLogin } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from 'src/app/shared/services/user.service';
import { MustMatch, MustNotMatch } from '../_helpers/must-match.validator';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup;
  submitted = false;
  regModelforPassword: RegExpModel = new RegExpModel();

  credentials: Credentials = new Credentials();

  regExpressionForPassword: RegExp = new RegExp(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/);

  constructor(
    private formBuilder: FormBuilder, 
    private userService: UserService,
    private authService: AuthService,
    private messageService: MessageService, 
    private primengConfig: PrimeNGConfig) {
    this.regModelforPassword.regex = this.regExpressionForPassword;
    this.regModelforPassword.errorMessage = 'Please provide a valid password';
     }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    if(this.authService.currentUserValue){
    this.changePasswordForm = this.formBuilder.group({
      OldPassword: ['', [Validators.required, Validators.minLength(8), Validators.pattern(this.regExpressionForPassword)]],
      NewPassword: ['', [Validators.required, Validators.minLength(8), Validators.pattern(this.regExpressionForPassword)]],
      ConfirmPassword: ['', Validators.required],
    },
      {
        validator: MustNotMatch('OldPassword', 'NewPassword'),
        Validator: MustMatch('NewPassword', 'ConfirmPassword')
      });

      var user = JSON.parse(window.localStorage.getItem('currentUser') || '{}');
   
      this.credentials.Email = user.UserEmail;
      this.credentials.PhoneNumber = user.UserPhoneNumber;
      this.credentials.Password = user.UserPassword;
    }
  }

  get f() { return this.changePasswordForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.changePasswordForm.invalid) {
      return;
    }
    var userData = this.changePasswordForm.value;
    var changePassword = new ChangePassword();
    changePassword.UserEmail = this.credentials.Email;
    changePassword.UserPhoneNumber = this.credentials.PhoneNumber;
    changePassword.OldPassword = userData.OldPassword;
    changePassword.NewPassword = userData.NewPassword;
    if (changePassword.OldPassword.match(this.regModelforPassword.regex)
    && changePassword.NewPassword.match(this.regModelforPassword.regex)
    ) {
      this.userService.postChangePassword(changePassword)
        .subscribe(res => {
          if (res.status == 201) {
            this.messageService.add({severity:'success', summary: 'Success', detail: 'Password changed successfully!'});
            this.onReset();
          }
          
        },
          err => {
            if (err.status == 500) {
              alert("Internal server error");
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

  onReset(){
    this.submitted = false;
    this.changePasswordForm.reset();
  }
}
