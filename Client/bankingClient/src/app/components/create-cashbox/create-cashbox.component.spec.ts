import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCashboxComponent } from './create-cashbox.component';

describe('CreateCashboxComponent', () => {
  let component: CreateCashboxComponent;
  let fixture: ComponentFixture<CreateCashboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateCashboxComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateCashboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
