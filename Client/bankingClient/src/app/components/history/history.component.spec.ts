import { ComponentFixture, TestBed } from '@angular/core/testing';

import { historyComponent } from './history.component';

describe('historyComponent', () => {
  let component: historyComponent;
  let fixture: ComponentFixture<historyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ historyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(historyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
