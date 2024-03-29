import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuctionCreateComponent } from './auction-create/auction-create.component';
import { AuctionPageComponent } from './auction-page/auction-page.component';
import {AuctionListComponent} from "./auction-list/auction-list.component";

const routes: Routes = [
  {
    path: '',
    component: AuctionListComponent
  },
  {
    path: 'create',
    component: AuctionCreateComponent
  },
  {
    path: 'edit/:id',
    component: AuctionCreateComponent
  },
  {
    path: 'view/:id',
    component: AuctionPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuctionRoutingModule { }
