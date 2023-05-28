import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { OfferRoutingModule } from '../offers/offer-routing.module';
import {HttpClientModule} from '@angular/common/http'



@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    OfferRoutingModule,
    HttpClientModule
  ],
  exports:[
    NavbarComponent,
  ]
})
export class CoreModule { }
