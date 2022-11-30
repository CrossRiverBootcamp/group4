import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CashboxDetailsComponent } from './cashbox-details.component';

describe('CashboxDetailsComponent', () => {
  let component: CashboxDetailsComponent;
  let fixture: ComponentFixture<CashboxDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CashboxDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CashboxDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
