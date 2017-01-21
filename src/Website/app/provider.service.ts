import { Injectable } from "@angular/core";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

import { CreateProviderCommand } from './createProviderCommand'

@Injectable()
export class ProviderService {

    private createProviderUrl = 'http://localhost:65023/api/providers';

    constructor(private http : Http) { }

    createProvider(command : CreateProviderCommand) : Promise<string> {
        return this.http.post(this.createProviderUrl, command)
            .toPromise()
            .then(response => response.json().ProviderId)
            .catch(this.handleError)
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}