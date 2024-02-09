import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { TextInputComponent } from './components/text-input/text-input.component';
import { GoogleSigninButtonModule, SocialLoginModule } from '@abacritt/angularx-social-login';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from '../app.routes';



@NgModule({
  declarations: [
    TextInputComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    AppRoutingModule,
    SocialLoginModule,
    GoogleSigninButtonModule,
  ],
  exports: [
    TextInputComponent,
    CommonModule,
    MaterialModule,
    GoogleSigninButtonModule,
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    SocialLoginModule,
    GoogleSigninButtonModule
  ]
})
export class SharedModule { }
