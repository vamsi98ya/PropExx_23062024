import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LiveTradesRoutingModule } from './live-trades-routing.module';
import { LiveTradesComponent } from './live-trades/live-trades.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    LiveTradesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LiveTradesRoutingModule
  ],
  providers:[MessageService]
})
export class LiveTradesModule { }
