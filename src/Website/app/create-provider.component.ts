import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CreateProviderCommand } from './create-provider-command';
import { ProviderService } from './provider.service'

@Component({
    selector: 'create-provider',
    templateUrl: 'app/create-provider.component.html',
    providers: [ProviderService]
})
export class CreateProviderComponent
{
    provider : CreateProviderCommand;

    constructor(
        private providerService : ProviderService,
        private router : Router
    )
    {
        this.provider = new CreateProviderCommand();
    }

    createProvider(command: CreateProviderCommand): void {
        this.providerService.createProvider(command)
            .then(x => this.router.navigate(['providers', x]));
    }
}