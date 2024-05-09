import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const httpRyanInterceptor: HttpInterceptorFn = (req, next) => {
  //Get the access token from localStorageSession -- Temporary implementation
  let token = localStorage.getItem("00000000-0000-0000-84d1-95697bafc8a6.9188040d-6c67-4c5b-b112-36a304b66dad-login.windows.net-idtoken-c1eed0b4-a59d-46bd-b3ee-416e35abbfa3-3e11228a-56f5-450d-9cf1-6283bdb7f12c---"); 
  let secret = token?.split(",")[4].split(":")[1].toString();
  secret = secret?.substring(1, secret.length - 1);
  
  const authToken = secret; 
  // Clone the request and add the authorization header
  const authReq = req.clone({
    setHeaders: {      
      Authorization: `Bearer ${authToken}`
    }
  });

  return next(authReq).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse) {
        // Handle HTTP errors
        if (err.status === 401) {
          // Specific handling for unauthorized errors         
          console.error('Unauthorized request:', err);
          // You might trigger a re-authentication flow or redirect the user here
        } else {
          // Handle other HTTP error codes
          console.error('HTTP error:', err);
        }
      } else {
        // Handle non-HTTP errors
        console.error('An error occurred:', err);
      }

      // Re-throw the error to propagate it further
      return throwError(() => err); 
    })
  );
};