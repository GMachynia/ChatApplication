import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { Router } from "@angular/router";

@Injectable()
export class LocalizationInterceptor implements HttpInterceptor {

    constructor(private router: Router) {
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('language') == null) {
            const clonedReq = req.clone({
            headers: req.headers.set('Accept-Language', 'en')
            });
            return next.handle(clonedReq);
        }
        else{
            const clonedReq = req.clone({
            headers: req.headers.set('Accept-Language', localStorage.getItem('language'))
        });
            return next.handle(clonedReq);
        }
        
    }
}