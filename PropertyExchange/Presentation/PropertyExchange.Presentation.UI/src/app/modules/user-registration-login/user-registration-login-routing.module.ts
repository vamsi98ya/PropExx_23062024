import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { UserDetailsComponent } from './user-details/user-details.component';

const routes: Routes = [
  {path:'',redirectTo:'Login',pathMatch:'full'},
  {path:'Login',component:LoginComponent},
  {path:'Register',component:RegistrationComponent},
  {path:'UserDetails',component:UserDetailsComponent, canActivate:[AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRegistrationLoginRoutingModule { }
