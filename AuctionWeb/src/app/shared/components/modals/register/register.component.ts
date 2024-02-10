import {Component, OnInit} from '@angular/core';
import {MatDialog, MatDialogActions, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {MatFormField} from "@angular/material/form-field";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {LoginComponent} from "../login/login.component";
import {AccountService} from "../../../../core/services/account.service";
import {ValidationsFn} from "../../../helpers/validations";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    MatDialogContent,
    MatFormField,
    FormsModule,
    MatDialogActions,
    MatButton,
    MatInput,
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit{
  public registerForm: FormGroup = new FormGroup({});

  constructor(private dialogRef: MatDialogRef<RegisterComponent>,
              private dialog: MatDialog,
              private fb: FormBuilder,
              private accountService: AccountService) { }

  public ngOnInit(): void {
    this.initializeForm();
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onRegister(): void {
    const newUser = this.registerForm.value;

    this.accountService.register(newUser)
      .subscribe({
        next: () => {
          this.dialogRef.close();
        }
      });
  }

  public openLoginModal(): void {
    this.dialogRef.close();
    this.dialog.open(LoginComponent, {
      width: '600px'
    });
  }

  private initializeForm() {
    this.registerForm = this.fb.group({
      username: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50)],
      ],
      fullName: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50)],
      ],
      dob: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50)],
      ],
      email: [
        '',
        [Validators.required, Validators.minLength(5), Validators.maxLength(50), ValidationsFn.emailMatch()],
      ],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(25),
        ValidationsFn.matchValues('password')]],
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
    });
  }
}
