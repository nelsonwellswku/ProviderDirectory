import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

import { Provider } from './provider';
import { CreateProviderCommand } from './create-provider-command'
import { PagedResult } from './paged-result';
import { GetProvidersQuery } from './get-providers-query'

@Injectable()
export class ProviderService {

    private providersCollectionUrl = 'http://localhost:65023/api/providers';

    constructor(private http: Http) { }

    getProvider(providerId: string): Promise<Provider> {
        return this.http.get(this.providersCollectionUrl + "/" + providerId)
            .toPromise()
            .then(response => {
                var provider = new Provider();
                var data: any = response.json();
                provider.npi = data.NPI;
                provider.firstName = data.FirstName;
                provider.lastName = data.LastName;
                return provider;
            })
            .catch(this.handleError);
    }

    createProvider(command: CreateProviderCommand): Promise<string> {
        return this.http.post(this.providersCollectionUrl, command)
            .toPromise()
            .then(response => response.json().ProviderId)
            .catch(this.handleError)
    }

    getProviders(query: GetProvidersQuery): Promise<PagedResult<Provider>> {
        var url = this.providersCollectionUrl +
            '?page=' +
            query.page +
            '&recordsPerPage=' +
            query.recordsPerPage;

        return this.http.get(url)
            .toPromise()
            .then(response => {
                var pagedResult = new PagedResult();
                pagedResult.items = new Array<Provider>();
                var responseJson = response.json();
                
                for(var i = 0; i < responseJson.Items.length; i++)
                {
                    var prov : Provider = {
                        providerId : responseJson.Items[i].ProviderId,
                        npi : responseJson.Items[i].NPI,
                        firstName : responseJson.Items[i].FirstName,
                        lastName : responseJson.Items[i].LastName
                    }

                    pagedResult.items.push(prov);
                }

                pagedResult.currentPage = responseJson.CurrentPage;
                pagedResult.currentRecordsPerPage = responseJson.CurrentRecordsPerPage;
                pagedResult.totalItems = responseJson.TotalItems;
                return pagedResult;
            })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}