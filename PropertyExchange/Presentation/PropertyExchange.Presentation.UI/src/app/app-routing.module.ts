import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertiesComponent } from './modules/properties/properties.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  {path:'',redirectTo:'Home',pathMatch:'full' },
  {path:'Home',loadChildren:() => import("./modules/home/home.module").then(module => module.HomeModule)},
  {path:'Properties', loadChildren:() => import("./modules/properties/properties.module").then(module => module.PropertiesModule)},
  {path:'AuthenticateUser',loadChildren:() => import("./modules/user-registration-login/user-registration-login.module").then(module => module.UserRegistrationLoginModule)},
  {path:'Live-Trades', loadChildren:() => import("./modules/live-trades/live-trades.module").then(module => module.LiveTradesModule)},
  {path:'PageNotFound',component:NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
