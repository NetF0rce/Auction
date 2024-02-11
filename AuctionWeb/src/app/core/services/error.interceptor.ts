import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, catchError } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    return next.handle(request).pipe(
      catchError(err => {
        if (err) {
          switch (err.status) {
            case 401:
              this.toastr.error('Unauthorized');
              break;
            case 403:
              this.toastr.error('Forbidden');
              break;
            case 422:
              this.toastr.error(Object.values(err.error).join('\n'))
              break;
            case 404:
              this.toastr.error('Not Found');
              break;
            case 500:
              this.toastr.error('Internal Server Error');
              break;
            default:
              this.toastr.error('Something went wrong');
              break;
          }

        }
        return new Observable<HttpEvent<unknown>>();
      }
      )
    );
  }
}
