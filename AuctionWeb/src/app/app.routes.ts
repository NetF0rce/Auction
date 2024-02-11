import {RouterModule, Routes} from '@angular/router';
import {NgModule} from "@angular/core";

export const routes: Routes = [
  {
    path: 'auctions',
    loadChildren: () => import('./modules/auction/auction.module').then((m) => m.AuctionModule),
  },
  { path: '', redirectTo: '/auctions', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
