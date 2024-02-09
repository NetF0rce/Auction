import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizationDeepLinkingService } from '../../../core/services/authorization-deep-linking.service';
import { FacebookLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Subscription, take } from 'rxjs';
import { AccountService } from '../../../core/services/account.service';
import { User } from '../../../models/User/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../_general-styles/authorization.scss', './login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  @Output() cancelRegister = new EventEmitter();
  returnUrl: string | undefined;
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  authorizationService: AuthorizationDeepLinkingService = new AuthorizationDeepLinkingService(this.route);
  private subscriptions: Subscription[] = []

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private socialAuthService: SocialAuthService) { }

  ngOnInit(): void {
    this.initializeForm();
    this.returnUrl = this.authorizationService.getReturnUrl();
    this.setCurrentUser();
    this.socialAuthService.authState
      .subscribe((user: SocialUser) => {
        if (user) {
          this.accountService.externalLogin({ idToken: user.idToken, provider: user.provider }).subscribe();
          this.router.navigate(['/login'])
        }
      });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
    this.subscriptions.push(this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    }));
  }

  login() {
    this.accountService.login(this.registerForm.value).pipe(take(1)).subscribe({
      next: response => {
        this.router.navigateByUrl(this.returnUrl || "/");
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }

  cancel() {
    this.router.navigateByUrl(this.returnUrl || "/");
  }

  signInWithFB(): void {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
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
