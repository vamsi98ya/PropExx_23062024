import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { WelcomeInvestorComponent } from './welcome-investor/welcome-investor.component';
import { WhoWeAreComponent } from './who-we-are/who-we-are.component';
import { WhatWeDoComponent } from './what-we-do/what-we-do.component';
import { HowWeWorkComponent } from './how-we-work/how-we-work.component';
import { WhatOurUsersAreSayingComponent } from './what-our-users-are-saying/what-our-users-are-saying.component';
import { UserDetailsComponent } from '../user-registration-login/user-details/user-details.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PersonalDetailsComponent } from '../user-registration-login/personal-details/personal-details.component';
import { ChangePasswordComponent } from '../user-registration-login/change-password/change-password.component';
import { AddFundsComponent } from '../user-registration-login/add-funds/add-funds.component';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { AboutUsComponent } from './about-us/about-us.component';
import { ChartModule } from 'primeng/chart';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    WelcomeInvestorComponent,
    WhoWeAreComponent,
    WhatWeDoComponent,
    HowWeWorkComponent,
    WhatOurUsersAreSayingComponent,
    UserDetailsComponent,
    PersonalDetailsComponent,
    ChangePasswordComponent,
    AddFundsComponent,
    AboutUsComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ReactiveFormsModule,
    DialogModule,
    TableModule,
    ChartModule,
    ToastModule
  ],
  providers:[MessageService]
})
export class HomeModule { }
