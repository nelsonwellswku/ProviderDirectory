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
    pagedResult: PagedResult<Provider>
    pagingUrl: string;

    ngOnInit(): void {
        this.pagingUrl = this.providerService.ProvidersCollectionUrl;

        this.providerService.getProviders({
            page: 1,
            recordsPerPage: 10
        })
        .then((pagedResult: PagedResult<Provider>) => {
            this.pagedResult = pagedResult;
            this.providers = pagedResult.items
        })
    }
}