import { Component } from '@angular/core';

import { Provider } from './provider';

@Component({
    selector: 'create-provider',
    templateUrl: 'app/create-provider.component.html',
    styleUrls: ['app/create-provider.component.css']
})
export class CreateProviderComponent
{
    provider : Provider;

    constructor()
    {
        this.provider = new Provider();
    }
}