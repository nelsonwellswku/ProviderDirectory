import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

import { Provider } from './provider';
import { CreateProviderForm } from './createProvider/create-provider-form'
import { PagedResult } from '../paged-result';
import { GetProvidersQuery } from './providerDetail/get-providers-query'

@Injectable()
export class ProviderService {

    ProvidersCollectionUrl = 'http://localhost:65023/api/providers';

    constructor(private http: Http) { }

    getProvider(providerId: string): Promise<Provider> {
        return this.http.get(this.ProvidersCollectionUrl + "/" + providerId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    createProvider(command: CreateProviderForm): Promise<string> {
        return this.http.post(this.ProvidersCollectionUrl, command)
            .toPromise()
            .then(response => response.json().ProviderId)
            .catch(this.handleError)
    }

    getProviders(query: GetProvidersQuery): Promise<PagedResult<Provider>> {
        var url = this.ProvidersCollectionUrl +
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