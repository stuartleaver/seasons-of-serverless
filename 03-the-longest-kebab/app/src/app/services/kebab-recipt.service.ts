import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class KebabReciptService {

  constructor(private http: HttpClient) { }

  getKebabRecipt() {
    return this.http.get<any>('http://localhost:7071/api/KebabCalculator?baseIngredientWeight=0.45');
  }
}
