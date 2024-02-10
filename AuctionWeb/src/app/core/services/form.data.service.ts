import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class FormDataService {
  static objectToFormData(obj: any): FormData {
    const formData = new FormData();

    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        formData.append(key, obj[key]);
      }
    }

    return formData;
  }
}
