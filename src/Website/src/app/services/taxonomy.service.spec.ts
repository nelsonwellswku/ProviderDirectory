import { TestBed, inject } from '@angular/core/testing';

import { TaxonomyService } from './taxonomy.service';

describe('TaxonomyService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TaxonomyService]
    });
  });

  it('should ...', inject([TaxonomyService], (service: TaxonomyService) => {
    expect(service).toBeTruthy();
  }));
});
