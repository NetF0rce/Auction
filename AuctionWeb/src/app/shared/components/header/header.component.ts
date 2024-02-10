import { Component, ElementRef, HostListener } from '@angular/core';
import { AccountService } from "../../../core/services/account.service";
import { Router, RouterLink } from "@angular/router";
import { AsyncPipe, NgIf } from "@angular/common";
import { AvatarComponent } from "../avatar/avatar.component";
import { MatFormField, MatInput, MatLabel } from "@angular/material/input";
import { MatButton } from "@angular/material/button";
import { MatDialog } from "@angular/material/dialog";
import { LoginComponent } from "../modals/login/login.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    AsyncPipe,
    RouterLink,
    AvatarComponent,
    NgIf,
    MatInput,
    MatButton,
    MatLabel,
    MatFormField
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  public isLoginModalOpen = false;
  public isRegisterModalOpen = false;

  public showDropdown = false;


  constructor(public accountService: AccountService,
    private el: ElementRef,
    private router: Router,
    public dialog: MatDialog) {

  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;

    if (!this.el.nativeElement.contains(target)) {
      this.showDropdown = false; // Сховати меню
    }
  }

  public toggleDropdown() {
    this.showDropdown = !this.showDropdown;
  }
  public openLoginModal() {
    const dialogRef = this.dialog.open(LoginComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  public openRegisterModal() {
    this.isRegisterModalOpen = true;
  }

  public closeLoginModal() {
    this.isLoginModalOpen = false;
  }

  public closeRegisterModal() {
    this.isRegisterModalOpen = false;
  }

  public logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/home');
  }

  public toRegister() {
    this.closeLoginModal();
    this.openRegisterModal();
  }

  public toLogin() {
    this.closeRegisterModal();
    this.openLoginModal();
  }

  public truncateText(text: string): string {
    return text.length > 7 ? text.substring(0, 7) + '...' : text;
  }
}
