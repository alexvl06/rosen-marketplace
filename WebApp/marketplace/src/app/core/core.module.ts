import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { OfferRoutingModule } from '../offers/offer-routing.module';



@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    OfferRoutingModule
  ],
  exports:[
    NavbarComponent,
  ]
})
export class CoreModule { }
