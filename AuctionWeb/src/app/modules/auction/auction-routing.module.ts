import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuctionListComponent } from "./auction-list/auction-list.component";
import { AuctionCreateComponent } from './auction-create/auction-create.component';

const routes: Routes = [
  {
    path: '',
    component: AuctionListComponent
  },
  {
    path: 'create',
<<<<<<< Updated upstream
=======
    component: AuctionCreateComponent
  },
  {
    path: 'edit/:id',
>>>>>>> Stashed changes
    component: AuctionCreateComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuctionRoutingModule { }
