import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OfferItemComponent } from "./offer-item/offer-item.component";
import { OfferCreationComponent } from "./offer-creation/offer-creation.component";
import { OfferListComponent } from "./offer-list/offer-list.component";
import { ReactiveFormsModule } from "@angular/forms";
import { CoreModule } from "../core/core.module";
import { PaginationComponent } from './offer-list/pagination/pagination.component';
import { OffersComponent } from './offers.component';
import { OfferRoutingModule } from "./offer-routing.module";


@NgModule({
  declarations: [
    OfferItemComponent,
    OfferCreationComponent,
    OfferListComponent,
    PaginationComponent,
    OffersComponent
  ],
  imports: [CommonModule, ReactiveFormsModule, CoreModule,
    OfferRoutingModule],
  exports:[OfferItemComponent]
})
export class OffersModule {}
