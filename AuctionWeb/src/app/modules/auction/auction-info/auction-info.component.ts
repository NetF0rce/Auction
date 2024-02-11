import { Component, Input, OnInit } from '@angular/core';
import { AuctionDto } from "../../../models/auction/auction-dto";
import { Router } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'auction-info',
  templateUrl: './auction-info.component.html',
  styleUrl: './auction-info.component.scss'
})
export class AuctionInfoComponent implements OnInit {
  // @ts-ignore
  @Input() auction: AuctionDto;
  @Input() selected?: boolean;

  role: string | null = null;

  public menuVisible = false;
  public menuOpen = false;


  constructor(private router: Router,
    private accountService: AccountService) { }

  ngOnInit(): void {
    console.log(this.auction);
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.role = user.userDto.role;
        }
      }
    })
  }
  editAuction() {
    this.router.navigate(['auctions/edit/' + this.auction?.id]);
  }
  viewAuction() {
    this.router.navigate(['auctions/view/' + this.auction?.id]);
  }
}
