import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { PagedResult } from 'app/paging/paged-result';
import { GetTaxonomiesQuery } from 'app/services/get-taxonomies-query';
import { Taxonomy } from 'app/services/taxonomy';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TaxonomyService {

  TaxonomiesCollectionUrl = 'http://localhost:65023/api/taxonomy';

  constructor(private http: Http) { }

  getTaxonomies(query: GetTaxonomiesQuery): Promise<PagedResult<Taxonomy>> {
    const url = this.TaxonomiesCollectionUrl +
      '?page=' +
      query.page +
      '&recordsPerPage=' +
      query.recordsPerPage;

    return this.http.get(url)
      .toPromise()
      .then(response => response.json())
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}
