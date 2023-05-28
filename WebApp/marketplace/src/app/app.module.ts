import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { OffersModule } from './offers/offers.module';
import { ImgURLCustomizerPipe } from './pipes/img-urlcustomizer.pipe';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    OffersModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
