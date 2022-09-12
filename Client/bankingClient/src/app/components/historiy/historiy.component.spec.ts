import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoriyComponent } from './historiy.component';

describe('HistoriyComponent', () => {
  let component: HistoriyComponent;
  let fixture: ComponentFixture<HistoriyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HistoriyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistoriyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
