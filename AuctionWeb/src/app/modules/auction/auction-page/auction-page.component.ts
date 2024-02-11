import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuctionDto } from '../../../models/auction/auction-dto';
import { environment } from '../../../../environments/environment';
import { ActivatedRoute } from '@angular/router';
import { BidsDto } from '../../../models/bids/bids-dto';
import { NgForm } from '@angular/forms';
import { AuctionUser } from '../../../models/auction/auction-user';

@Component({
  selector: 'auction-page',
  templateUrl: './auction-page.component.html',
  styleUrl: './auction-page.component.css',
})
export class AuctionPageComponent implements OnInit {

  auction: AuctionDto | undefined;
  id: string | null = this.route.snapshot.paramMap.get('id');
  bids: BidsDto[] | undefined;
  newBidAmount: number | undefined;
  users: AuctionUser[] | null = [];

  constructor(
    private readonly httpClient: HttpClient,
    private readonly route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.loadAuction();
    this.loadBids();
  }

  loadAuction(): void {
    this.httpClient.get<AuctionDto>(environment.apiUrl + 'auctions/' + this.id).subscribe({
      next: auction => {
        this.auction = auction;
      }
    });
  }

  loadBids(): void {
    this.httpClient.get<BidsDto[]>(environment.apiUrl + 'bids/' + this.id).subscribe({
      next: bids => {
        this.bids = bids;
        this.users = bids.map(bid => {
          return {
            id: bid.bidderId,
            name: bid.bidderName
          };
        });
      }
    });
  }

  submitBid(form: NgForm): void {
    if (form.valid && this.newBidAmount) {
      this.httpClient.post(environment.apiUrl + 'bids', { auctionId: this.id, amount: this.newBidAmount }).subscribe({
        next: () => {
          this.loadBids();
          form.resetForm();
        },
        error: err => {
          console.error('Failed to submit bid:', err);
        }
      });
    }
  }
}
