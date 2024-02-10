import {Component, OnInit} from '@angular/core';
import {MatDialog, MatDialogActions, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {RegisterComponent} from "../register/register.component";
import {ValidationsFn} from "../../../helpers/validations";
import {AccountService} from "../../../../core/services/account.service";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatDialogContent,
    MatFormField,
    MatDialogActions,
    MatInput,
    MatButton,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  public email: string = '';
  public password: string = '';
  public loginForm: FormGroup = new FormGroup({});

  constructor(private dialogRef: MatDialogRef<LoginComponent>,
              private dialog: MatDialog,
              private fb: FormBuilder,
              private accountService: AccountService) { }

  public ngOnInit(): void {
    this.initializeForm();
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onLogin(): void {
    const user = this.loginForm.value;

    this.accountService.login(user)
      .subscribe({
        next: () => {
          this.dialogRef.close();
        }
      })
  }

  public onRegister(): void {
    this.dialogRef.close();
    this.dialog.open(RegisterComponent, {
      width: '600px'
    });
  }

  private initializeForm() {
    this.loginForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50), ValidationsFn.emailMatch()],
      ],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
    });
  }
}
