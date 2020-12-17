import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { BarbequeCostResult } from '../components/big-barbecue/barbeque-cost-result';

@Injectable({
  providedIn: 'root'
})
export class BarbecueCostService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getBarbecueCosting(formData): Observable<any> {
    const url = `/api/barbecuecost`;
    return this.http.post<BarbequeCostResult>(url, formData).pipe(
      tap(_ => this.log(`fetched barbecue costing`)),
      catchError(
        (error: HttpErrorResponse): Observable<any> => {
            // we expect 404, it's not a failure for us.
            if (error.status === 404) {
                return of(null);
            }

            throw new Error(`Opps, something went wrong.`);
        },
    ),
    );
  }

  private log(message: string) {
    console.log(message);
  }
}
