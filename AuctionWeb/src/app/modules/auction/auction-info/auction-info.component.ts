import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatMenu, MatMenuItem} from "@angular/material/menu";
import {MatCard, MatCardActions, MatCardContent, MatCardHeader, MatCardSubtitle} from "@angular/material/card";
import {DatePipe, NgClass, NgIf} from "@angular/common";
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {AuctionDto} from "../../../models/Auction/auction-dto";

@Component({
  selector: 'app-auction-info',
  standalone: true,
  imports: [
    MatMenu,
    MatCardHeader,
    MatCard,
    NgClass,
    MatIcon,
    MatCardContent,
    DatePipe,
    MatCardActions,
    MatCardSubtitle,
    MatMenuItem,
    MatIconButton,
    NgIf
  ],
  templateUrl: './auction-info.component.html',
  styleUrl: './auction-info.component.scss'
})
export class AuctionInfoComponent {
  @Input() auction?: AuctionDto;
  @Input() selected?: boolean;

  public menuVisible = false;
  public menuOpen = false;
}
