import { TestBed } from '@angular/core/testing';

import { CreateCashboxService } from './create-cashbox.service';

describe('CreateCashboxService', () => {
  let service: CreateCashboxService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateCashboxService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
