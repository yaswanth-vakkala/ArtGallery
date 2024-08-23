import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  if(shouldInterceptRequest(req)){
    const authHeader = inject(CookieService).get('Authorization');
    const reqWithHeader = req.clone({
    headers: req.headers.set('Authorization', authHeader),
  });
    return next(reqWithHeader);
  }
    return next(req);
};

function shouldInterceptRequest(request: HttpRequest<any>): boolean {
  return request.urlWithParams.indexOf('addAuth=true', 0) > -1? true: false;
}