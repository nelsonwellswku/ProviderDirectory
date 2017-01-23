import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

import { Provider } from './provider';
import { CreateProviderCommand } from './create-provider-command'

@Injectable()
export class ProviderService {

    private providersCollectionUrl = 'http://localhost:65023/api/providers';

    constructor(private http : Http) { }

    getProvider(providerId : string) : Promise<Provider> {
        return this.http.get(this.providersCollectionUrl + "/" + providerId)
        .toPromise()
        .then(response => {
            var provider = new Provider();
            var data : any = response.json();
            provider.npi = data.NPI;
            provider.firstName = data.FirstName;
            provider.lastName = data.LastName;
            return provider;
        })
        .catch(this.handleError);
    }

    createProvider(command : CreateProviderCommand) : Promise<string> {
        return this.http.post(this.providersCollectionUrl, command)
            .toPromise()
            .then(response => response.json().ProviderId)
            .catch(this.handleError)
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}