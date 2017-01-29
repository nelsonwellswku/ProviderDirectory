import { Component, OnInit } from '@angular/core'


import { Provider } from './provider';
import { ProviderService } from './provider.service';
import { PagedResult } from './paged-result';

@Component({
    selector: 'list-providers',
    templateUrl: 'app/list-providers.component.html',
    providers: [ProviderService]
})
export class ListProvidersComponent implements OnInit {
    constructor(private providerService: ProviderService) { }

    providers: Provider[];

    ngOnInit(): void {
        this.providerService.getProviders({
            page: 1,
            recordsPerPage: 10
        })
        .then((pagedResult: PagedResult<Provider>) => this.providers = pagedResult.items)
    }
}