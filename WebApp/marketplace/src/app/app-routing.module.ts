import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './authentication/login/login.component';


const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'offer', loadChildren: ()=>import('./offers/offers.module').then((m)=>m.OffersModule)}

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
