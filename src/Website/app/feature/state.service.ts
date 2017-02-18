import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import { State } from "./state";

@Injectable()
export class StateService {

    StatesCollectionUrl = 'http://localhost:65023/api/states';

    constructor(private http : Http) { }

    getStates(): Promise<State[]> {
        return this.http.get(this.StatesCollectionUrl)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}