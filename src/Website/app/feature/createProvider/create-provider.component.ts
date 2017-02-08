import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CreateProviderForm } from './create-provider-form';
import { ProviderService } from '../provider.service'

@Component({
    selector: 'create-provider',
    templateUrl: 'app/feature/createProvider/create-provider.component.html',
    providers: [ProviderService]
})
export class CreateProviderComponent
{
    provider : CreateProviderForm;

    constructor(
        private providerService : ProviderService,
        private router : Router
    )
    {
        this.provider = new CreateProviderForm();
    }

    createProvider(form: CreateProviderForm): void {
        this.providerService.createProvider(form)
            .then(x => this.router.navigate(['providers', x]));
    }
}