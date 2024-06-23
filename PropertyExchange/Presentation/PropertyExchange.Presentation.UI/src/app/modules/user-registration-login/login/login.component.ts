import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService, PrimeNGConfig } from 'primeng/api';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  regModelforEmail: RegExpModel = new RegExpModel();
  regModelforPhoneNumber: RegExpModel = new RegExpModel();
  regModelforPassword: RegExpModel = new RegExpModel();
  regModelforEmailorPhoneNumber: RegExpModel = new RegExpModel();

  regExpressionForEmail: RegExp = new RegExp(/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/);
  regExpressionForPhoneNumber: RegExp = new RegExp(/^\d{10}$/);
  regExpressionForPassword: RegExp = new RegExp(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/);
  regExpressionForEmailorPhoneNumber: RegExp = new RegExp(/^(?:\d{10}|\w+@\w+\.\w{2,3})$/);

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService, 
    private primengConfig: PrimeNGConfig) {
      this.regModelforEmail.regex = this.regExpressionForEmail;
    this.regModelforEmail.errorMessage = 'Please provide a valid email address';

    this.regModelforPhoneNumber.regex = this.regExpressionForPhoneNumber;
    this.regModelforPhoneNumber.errorMessage = 'Please provide a valid phone number';

    this.regModelforPassword.regex = this.regExpressionForPassword;
    this.regModelforPassword.errorMessage = 'Please provide a valid password';

    this.regModelforEmailorPhoneNumber.regex = this.regExpressionForEmailorPhoneNumber;
    this.regModelforEmailorPhoneNumber.errorMessage = 'Please provide a valid email or phone number';
     }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    if(this.authService.currentUserValue){
      this.authService.logout();
    }
    this.loginForm = this.formBuilder.group({
      EmailorPhoneNumber: ['', [Validators.required, Validators.pattern(this.regExpressionForEmailorPhoneNumber)]],
      Password: ['', [Validators.required, Validators.minLength(8), Validators.pattern(this.regExpressionForPassword)]],
      saveCredentials: [false]
    });
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    var credentials = new Credentials();
    
    if (this.loginForm.value.EmailorPhoneNumber.match(this.regModelforEmail.regex)){
      credentials.Email = this.loginForm.value.EmailorPhoneNumber;
      credentials.PhoneNumber = "";
    }
    else if(this.loginForm.value.EmailorPhoneNumber.match(this.regModelforPhoneNumber.regex)){
      credentials.PhoneNumber = this.loginForm.value.EmailorPhoneNumber;
      credentials.Email = "";
    }

    if(this.loginForm.value.Password.match(this.regModelforPassword.regex)){
    credentials.Password = this.loginForm.value.Password;
    }

    if ((credentials.Email.match(this.regModelforEmail.regex)
    || credentials.PhoneNumber.match(this.regModelforPhoneNumber.regex))
    && credentials.Password.match(this.regModelforPassword.regex)) 
    {
    this.authService.login(credentials as Credentials)
      .subscribe(res => {
        if (res.status == 200) {
          //this.toastr.success('Successfully', 'Login');
          //alert("User logged in successfully!");
          this.router.navigate(['/Home/WelcomeInvestor']);
        }
        else {
          //alert(res.status);
          //this.toastr.error(res.status.toString(), 'Login');
        }
      },
        err => {
          //this.toastr.error('Error Code:' + err.status.toString(), 'Invalid Credentials');
          this.messageService.add({severity:'error', summary: 'Error', detail: 'Invalid Credentials!'});
        }
      )
  }
}

  onReset() {
    this.submitted = false;
    this.loginForm.reset();
  }

}
