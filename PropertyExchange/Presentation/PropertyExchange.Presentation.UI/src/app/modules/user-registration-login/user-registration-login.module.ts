import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRegistrationLoginRoutingModule } from './user-registration-login-routing.module';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    CommonModule,
    UserRegistrationLoginRoutingModule,
    FormsModule, ReactiveFormsModule, ToastModule  ],
    providers:[MessageService]
})
export class UserRegistrationLoginModule { }
