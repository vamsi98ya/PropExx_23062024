import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { AboutUsComponent } from './about-us/about-us.component';
import { WelcomeInvestorComponent } from './welcome-investor/welcome-investor.component';

const routes: Routes = [
  {path:'', children:[
    {path:'',redirectTo:'WelcomeInvestor', pathMatch:'full'},
    {path:'WelcomeInvestor', component:WelcomeInvestorComponent, canActivate:[AuthGuard]},
    {path:'AboutUs', component:AboutUsComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
