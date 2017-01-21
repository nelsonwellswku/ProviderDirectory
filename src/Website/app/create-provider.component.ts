import { Component } from '@angular/core';

import { CreateProviderCommand } from './CreateProviderCommand';
import { ProviderService } from './provider.service'

@Component({
    selector: 'create-provider',
    templateUrl: 'app/create-provider.component.html',
    styleUrls: ['app/create-provider.component.css'],
    providers: [ProviderService]
})
export class CreateProviderComponent
{
    provider : CreateProviderCommand;

    constructor(private providerService : ProviderService)
    {
        this.provider = new CreateProviderCommand();
    }

    createProvider(command: CreateProviderCommand): void {
        this.providerService.createProvider(command);
    }
}