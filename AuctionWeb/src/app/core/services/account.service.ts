import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, map } from 'rxjs';
import { User } from '../../models/User/user';
import { ActivatedRoute, Router } from '@angular/router';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { environment } from '../../../environments/environment';
import { ExternalAuthDto } from '../../models/User/externalAuthDto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  private authChangeSub = new Subject<boolean>();
  private extAuthChangeSub = new Subject<SocialUser>();
  public authChanged = this.authChangeSub.asObservable();
  public extAuthChanged = this.extAuthChangeSub.asObservable();

  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, private externalAuthService: SocialAuthService) {
    this.externalAuthService.authState.subscribe((user) => {
      console.log(user)
      this.extAuthChangeSub.next(user);
    })
  }

  signInWithGoogle = () => {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
  signOutExternal = () => {
    this.externalAuthService.signOut();
  }

  externalLogin = (body: ExternalAuthDto) => {
    return this.http.post<User>(this.baseUrl + "auth/external-login", body).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'auth/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'auth/register', model).pipe(
      map(response => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.externalAuthService.signOut();
  }

  isCurrentUserAdmin() {
    return this.currentUser$.pipe(map(u => u?.userDto.role.toLowerCase().includes('admin')));
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
