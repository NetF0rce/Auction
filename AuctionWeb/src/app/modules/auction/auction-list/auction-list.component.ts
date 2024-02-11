import {Component, OnInit} from '@angular/core';
import {AuctionService} from "../../../core/services/auction.service";
// @ts-ignore
import {AuctionDto} from "../../models/auction/auction-dto";

@Component({
  selector: 'auction-list',
  templateUrl: './auction-list.component.html',
  styleUrl: './auction-list.component.scss'
})
export class AuctionListComponent implements OnInit {
  public auctions: AuctionDto[] = [];

  constructor(private readonly auctionService: AuctionService) {
  }

  ngOnInit(): void {
    this.auctionService.getAuctions()
      .subscribe({
        next: value => {
          this.auctions = value.data;
        }
      });
  }
}
