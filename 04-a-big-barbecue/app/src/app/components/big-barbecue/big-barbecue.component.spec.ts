import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { BarbecueCostService } from 'src/app/services/barbecue-cost.service';

import { BigBarbecueComponent } from './big-barbecue.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('BigBarbecueComponent', () => {
  let service: BarbecueCostService;
  let httpMock: HttpTestingController;

  let component: BigBarbecueComponent;
  let fixture: ComponentFixture<BigBarbecueComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, FormsModule, ReactiveFormsModule],
    });
    service = TestBed.inject(BarbecueCostService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BigBarbecueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
