import { Component, OnInit } from '@angular/core'
import { Router, ActivatedRoute, Params } from '@angular/router';

import { Provider } from './feature/provider';
import { ProviderService } from './feature/provider.service';
import { PagedResult } from './paged-result';

@Component({
    selector: 'list-providers',
    templateUrl: 'app/list-providers.component.html',
    providers: [ProviderService]
})
export class ListProvidersComponent implements OnInit {
    constructor(private providerService: ProviderService, private route: ActivatedRoute) { }

    providers: Provider[];
    pagedResult: PagedResult<Provider>
    pagingUrl: string;

    ngOnInit(): void {
        this.pagingUrl = "/providers";

        this.route.params.subscribe((params: Params) => {
            this.providerService.getProviders({
               page: params['page'] || 1,
               recordsPerPage: params['recordsPerPage'] || 10
            })
           .then((pagedResult: PagedResult<Provider>) => {
               this.pagedResult = pagedResult;
               this.providers = pagedResult.items
           })
        });
    }
}