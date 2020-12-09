import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
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
      catchError(this.handleError<any>(`getKebabRecipe baseIngredientWeight=${baseIngredientWeight}`))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(message);
  }
}
