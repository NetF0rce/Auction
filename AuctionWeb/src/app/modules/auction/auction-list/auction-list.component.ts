import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuctionDto } from '../../../models/Auction/auction-dto';
import { MyResponse } from '../../../models/common/response.model';

@Component({
  selector: 'auction-list',
  templateUrl: './auction-list.component.html',
  styleUrl: './auction-list.component.scss'
})
export class AuctionListComponent implements OnInit {
  auctions: AuctionDto[] = [];

  constructor(private readonly httpClient: HttpClient) {}

  ngOnInit(): void {
    this.getAuctions();
  }

  public getAuctions() {
    this.httpClient.get<MyResponse<AuctionDto>>(environment.apiUrl + 'auctions').subscribe((response: MyResponse<AuctionDto>) => {
      this.auctions = response.data;
    });
  }
}
