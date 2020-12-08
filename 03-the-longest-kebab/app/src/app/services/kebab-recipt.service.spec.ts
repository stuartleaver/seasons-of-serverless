import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { KebabReciptService } from './kebab-recipt.service';

describe('KebabReciptService', () => {
  let service: KebabReciptService;
  let httpMock: HttpTestingController;

  const mockKebabRecipe = JSON.parse('{ "weight": "0.45", "length": 4, "length": { "length": 73.27, "unit": 1 }, "ingredients": [ { "name": "Base", "unit": 1, "amount": "0.45" } ] }');

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(KebabReciptService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getKebabRecipt() should return data', () => {
    service.getKebabRecipt().subscribe((res) => {
      expect(res).toEqual(mockKebabRecipe);
    });

    const req = httpMock.expectOne('http://localhost:7071/api/KebabCalculator?baseIngredientWeight=0.45');
    expect(req.request.method).toBe('GET');
    req.flush(mockKebabRecipe);
  });
});
