import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuctionRoutingModule } from './auction-routing.module';
import { AuctionListComponent } from './auction-list/auction-list.component';
import { MaterialModule } from '../../material/material.module';
import { AuctionInfoComponent } from './auction-info/auction-info.component';
import { AuctionCreateComponent } from './auction-create/auction-create.component';
import { AuctionPageComponent } from './auction-page/auction-page.component';

@NgModule({
  declarations: [
    AuctionListComponent,
    AuctionInfoComponent,
    AuctionCreateComponent,
    AuctionPageComponent
  ],
  imports: [
    CommonModule,
    AuctionRoutingModule,
    MaterialModule,
  ],
  exports: [
    AuctionListComponent
  ]
})
export class AuctionModule { }
