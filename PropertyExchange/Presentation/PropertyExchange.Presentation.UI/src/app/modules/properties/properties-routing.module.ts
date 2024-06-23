import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllPropertiesComponent } from './all-properties/all-properties.component';
import { PropertyDetailsComponent } from './property-details/property-details.component';

const routes: Routes = [
  {path:'',redirectTo:'All-Properties',pathMatch:'full'},
  {path:'All-Properties',component:AllPropertiesComponent},
  { path: ':id', component: PropertyDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PropertiesRoutingModule { }
