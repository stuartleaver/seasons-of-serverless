import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { KebabRecipe } from '../components/longest-kebab/kebab-recipe';

@Injectable({
  providedIn: 'root'
})
export class KebabRecipeService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getKebabRecipe(baseIngredientWeight: number): Observable<KebabRecipe> {
    const url = `http://localhost:7071/api/KebabCalculator?baseIngredientWeight=${baseIngredientWeight}`;
    return this.http.get<KebabRecipe>(url).pipe(
      tap(_ => this.log(`fetched kebab recipe baseIngredientWeight=${baseIngredientWeight}`)),
      catchError(
        (error: HttpErrorResponse): Observable<any> => {
            // we expect 404, it's not a failure for us.
            if (error.status === 404) {
                return of(null);
            }

            throw new Error(`Opps, something went wrong. ${error.error}`);            
        },
    ),
    );
  }

  private log(message: string) {
    console.log(message);
  }
}
