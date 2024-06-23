import { NgModule } from '@angular/core';
import { CommonModule, NgIf  } from '@angular/common';
import { PropertiesRoutingModule } from './properties-routing.module';
import { AllPropertiesComponent } from './all-properties/all-properties.component';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'primeng/accordion';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CarouselModule } from 'primeng/carousel';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PropertyDetailsComponent } from './property-details/property-details.component';
import { PerformanceGraphComponent } from './performance-graph/performance-graph.component';
import { ChartModule } from 'primeng/chart';
import { PercentageChangePipe } from 'src/app/shared/pipes/percentage-change.pipe';
import { DialogModule } from 'primeng/dialog';
import { AvatarModule } from 'primeng/avatar';
import {FieldsetModule} from 'primeng/fieldset';
import {ToastModule} from 'primeng/toast';
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    AllPropertiesComponent,
    PropertyDetailsComponent,
    PerformanceGraphComponent,
    PercentageChangePipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PropertiesRoutingModule,
    TableModule,
    TagModule,
    ButtonModule,
    PaginatorModule,
    ToggleButtonModule,
    OverlayPanelModule,
    InputTextModule,
    FormsModule,
    AccordionModule,
    AutoCompleteModule,
    CarouselModule,
    CascadeSelectModule,
    ProgressSpinnerModule,
    ChartModule,
    DialogModule,
    AvatarModule,
    FieldsetModule,
    ToastModule,
    NgIf     
  ],
  providers:[MessageService]
})
export class PropertiesModule { }
