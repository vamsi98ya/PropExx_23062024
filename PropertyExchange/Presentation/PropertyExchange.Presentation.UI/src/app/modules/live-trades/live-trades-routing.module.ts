import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { LiveTradesComponent } from './live-trades/live-trades.component';

const routes: Routes = [
  {path:'',redirectTo:'Orders',pathMatch:'full'},
  {path:'Orders',component:LiveTradesComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LiveTradesRoutingModule { }
