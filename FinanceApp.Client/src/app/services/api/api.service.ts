import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  get(url: string): Observable<any> {
    return this.http
      .get(`${environment.API_URL}${url}`)
      .pipe(
        map(data => data),
        catchError(err => this.handleError(err)
        )
      );
  }

  post(url: string, payload: any): Observable<any> {
    return this.http
      .post(`${environment.API_URL}${url}`, payload)
      .pipe(
        map(data => data),
        catchError(err => this.handleError(err)
        )
      );
  }

  handleError(error: any) {
    return throwError({
      "code": error.status,
      "error": error.error.error,
      "message": error.error.message,
      "key": error.error.key
    });
  }
}
