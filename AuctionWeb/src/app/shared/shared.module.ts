import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { TextInputComponent } from './components/text-input/text-input.component';
import { GoogleSigninButtonModule } from '@abacritt/angularx-social-login';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [
    TextInputComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
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
  ]
})
export class SharedModule { }
