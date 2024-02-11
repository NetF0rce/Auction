import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './core/services/account.service';
import { Router } from '@angular/router';
import { User } from './models/User/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AuctionWeb';

  constructor(private accountService: AccountService, private router: Router, private socialAuthService: SocialAuthService) {
  }

  ngOnInit(): void {
    this.setCurrentUser();
    this.socialAuthService.authState
      .subscribe((user: SocialUser) => {
        if (user) {
          this.accountService.externalLogin({ idToken: user.idToken, provider: user.provider }).subscribe();
        }
      });
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

  getState(outlet: any) {
    return outlet.isActivated ? outlet.activatedRoute.snapshot.url[0].path : "";
  }
}
