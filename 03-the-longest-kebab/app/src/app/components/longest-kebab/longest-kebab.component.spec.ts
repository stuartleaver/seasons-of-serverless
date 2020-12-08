import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { LongestKebabComponent } from './longest-kebab.component';

describe('LongestKebabComponent', () => {
  let component: LongestKebabComponent;
  let fixture: ComponentFixture<LongestKebabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LongestKebabComponent ],
      imports: [ HttpClientTestingModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LongestKebabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
