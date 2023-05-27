import { RouterModule, Routes } from "@angular/router";
import { OfferListComponent } from "./offer-list/offer-list.component";
import { OfferCreationComponent } from "./offer-creation/offer-creation.component";
import { NgModule } from "@angular/core";
import { OffersComponent } from "./offers.component";

const routes:Routes = [
  {path:'', component: OffersComponent,
  children:[
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
    {path: 'list', component: OfferListComponent},
    {path: 'creation', component: OfferCreationComponent}

  ]
},

]

@NgModule({
  imports:[RouterModule.forChild(routes)],
  exports:[RouterModule]
})
export class OfferRoutingModule{}
