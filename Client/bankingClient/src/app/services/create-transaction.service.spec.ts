import { TestBed } from '@angular/core/testing';

import { CreateTransactionService } from './create-transaction.service';

describe('CreateTransactionService', () => {
  let service: CreateTransactionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateTransactionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
