import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { KebabRecipeService } from './kebab-recipe.service';

describe('KebabRecipeService', () => {
  let service: KebabRecipeService;
  let httpMock: HttpTestingController;

  const mockKebabRecipe = JSON.parse('{ "weight": "0.45", "length": 4, "length": { "length": 73.27, "unit": 1 }, "ingredients": [ { "name": "Base", "unit": 1, "amount": "0.45" } ] }');

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(KebabRecipeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getKebabRecipe() should return data', () => {
    service.getKebabRecipe(0.45).subscribe((res) => {
      expect(res).toEqual(mockKebabRecipe);
    });

    const req = httpMock.expectOne('http://localhost:7071/api/KebabCalculator?baseIngredientWeight=0.45');
    expect(req.request.method).toBe('GET');
    req.flush(mockKebabRecipe);
  });
});
