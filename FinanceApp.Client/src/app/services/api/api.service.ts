import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient,
    private router: Router
  ) { }

  get(url: string): Observable<any> {

    return this.http
      .get(`${environment.API_URL}${url}`, { withCredentials: true })
      .pipe(
        map(data => data),
        catchError(err => this.handleError(err)
        )
      );
  }

  post(url: string, payload: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      withCredentials: true
    };

    return this.http
      .post(`${environment.API_URL}${url}`, payload, httpOptions)
      .pipe(
        map(data => data),
        catchError(err => this.handleError(err)
        )
      );
  }

  handleError(error: any) {
    if (error.status == 401) {
      this.router.navigate(['/login']);
    }

    return throwError({
      "code": error.status,
      "error": error.error.error,
      "message": error.error.message,
      "key": error.error.key
    });
  }
}
