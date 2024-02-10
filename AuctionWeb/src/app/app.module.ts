import { NgModule } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppRoutingModule } from './app.routes';
import { AppComponent } from "./app.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { GoogleLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { AuthModule } from './modules/auth/auth.module';
import { HeaderComponent } from "./shared/components/header/header.component";
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './core/services/token.interceptor';
import { AuctionModule } from './modules/auction/auction.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    SharedModule,
    AppRoutingModule,
    RouterOutlet,
    FormsModule,
    CoreModule,
    HeaderComponent,
    ReactiveFormsModule,
    AuctionModule
  ],
  providers: [
    provideAnimationsAsync(),
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('1038141147992-9tbh1kiha40f9cb7s5ve10rttgusfa8k.apps.googleusercontent.com')
          },
        ]
      } as SocialAuthServiceConfig,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
