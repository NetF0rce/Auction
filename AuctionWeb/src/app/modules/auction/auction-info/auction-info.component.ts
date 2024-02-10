import { Component, HostListener, Input, OnInit } from '@angular/core';
import { AuctionDto } from "../../../models/Auction/auction-dto";
import { Router } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'auction-info',
  templateUrl: './auction-info.component.html',
  styleUrl: './auction-info.component.scss'
})
export class AuctionInfoComponent implements OnInit {
  @Input() auction?: AuctionDto;
  @Input() selected?: boolean;

  role: string | null = null;

  public menuVisible = false;
  public menuOpen = false;

  @HostListener("click") onClick() {
    console.log("User Click using Host Listener")
  }

  constructor(private router: Router,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.role = user.userDto.role;
        }
      }
    })
  }
  editAuction() {
    this.router.navigate(['auctions', this.auction?.id]);
  }
}
