import { Component, HostListener, Input } from '@angular/core';
import { AuctionDto } from "../../../models/Auction/auction-dto";

@Component({
  selector: 'auction-info',
  templateUrl: './auction-info.component.html',
  styleUrl: './auction-info.component.scss'
})
export class AuctionInfoComponent {
  // @Input() auction?: AuctionDto;
  // @Input() selected?: boolean;

  public menuVisible = false;
  public menuOpen = false;
  @HostListener("click") onClick() {
    console.log("User Click using Host Listener")
  }

}
