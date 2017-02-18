import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CreateProviderForm } from './create-provider-form';
import { ProviderService } from '../provider.service'
import { StateService } from "../state.service";
import { State } from "../state";

@Component({
    selector: 'create-provider',
    templateUrl: 'app/feature/createProvider/create-provider.component.html',
    providers: [ProviderService, StateService]
})
export class CreateProviderComponent implements OnInit
{
    provider : CreateProviderForm;
    states : State[];

    constructor(
        private providerService : ProviderService,
        private stateService : StateService,
        private router : Router
    )
    {
        this.provider = new CreateProviderForm();
    }

    createProvider(form: CreateProviderForm): void {
        this.providerService.createProvider(form)
            .then(x => this.router.navigate(['providers', x]));
    }

    ngOnInit() {
        this.stateService.getStates()
        .then(states => this.states = states);
    }
}