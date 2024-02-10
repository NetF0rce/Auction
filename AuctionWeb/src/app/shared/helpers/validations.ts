import {AbstractControl, ValidatorFn} from "@angular/forms";

export class ValidationsFn {
  static matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) =>
      (control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true });
  }

  static emailMatch(): ValidatorFn {
    return (control: AbstractControl) =>
      (/^(?!\.)[A-Z0-9._-]+@(?!\.)[A-Z0-9.-]+\.[A-Z]+(?<!\.)$/i.test(control.value)
      && !/[._-]{2,}/.test(control.value)
        ? null : { emailMatch: true });
  }
}
