import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Provider } from 'app/services/provider';
import { PagedResult } from 'app/paging/paged-result';

import 'rxjs/add/operator/toPromise';
import { CreateProviderForm } from 'app/services/create-provider-form';
import { GetProvidersQuery } from 'app/services/get-providers-query';

@Injectable()
export class ProviderService {

  ProvidersCollectionUrl = 'http://localhost:65023/api/providers';

    constructor(private http: Http) { }

    getProvider(providerId: string): Promise<Provider> {
        return this.http.get(this.ProvidersCollectionUrl + '/' + providerId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    createProvider(command: CreateProviderForm): Promise<string> {
        return this.http.post(this.ProvidersCollectionUrl, command)
            .toPromise()
            .then(response => response.json().providerId)
            .catch(this.handleError);
    }

    getProviders(query: GetProvidersQuery): Promise<PagedResult<Provider>> {
        const url = this.ProvidersCollectionUrl +
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
