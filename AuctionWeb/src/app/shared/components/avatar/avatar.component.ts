import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';
import {NgIf, NgStyle, TitleCasePipe} from "@angular/common";

@Component({
  selector: 'app-avatar',
  standalone: true,
  imports: [
    NgStyle,
    TitleCasePipe,
    NgIf
  ],
  templateUrl: './avatar.component.html',
  styleUrl: './avatar.component.scss'
})
export class AvatarComponent implements OnChanges{
  @Input() public photoUrl : string | undefined;
  @Input() public name : string | undefined;
  @Input() public size: string | number = 50;

  public showInitials = false;
  public initials = '';
  public circleColor = '';
  public color = ['#EB7181', '#468547', '#FFD558' ,'#3670B2'];

  ngOnChanges(changes: SimpleChanges) {
    if (changes['photoUrl'] || changes['name']) {
      this.generatePhoto();
    }
  }

  public getFontSize(): number {
    return Number(this.size) * 0.4;
  }

  private createInitials(){
    if (!this.name){
      return;
    }
    const words = this.name.split(' ').filter(word => word);
    let initials = '';
    const maxInitials = 2;

    for (const word of words) {
      initials += word[0];

      if (initials.length === maxInitials) {
        break;
      }
    }
    this.initials = initials;
  }

  private generatePhoto(){
    if (!this.photoUrl){
      this.showInitials = true;
      this.createInitials();

      const randomIndex = Math.floor(Math.random()) * Math.floor(this.color.length);
      this.circleColor = this.color[randomIndex];
    }
    else {
      this.showInitials = false;
    }
  }
}
