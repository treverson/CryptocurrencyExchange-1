import { TestBed, inject } from '@angular/core/testing';

import { TransactionsResolver } from './transactions-resolver.service';

describe('TransactionsResolver', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TransactionsResolver]
    });
  });

  it('should be created', inject([TransactionsResolver], (service: TransactionsResolver) => {
    expect(service).toBeTruthy();
  }));
});
