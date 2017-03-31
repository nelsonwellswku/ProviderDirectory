import { Component, OnInit } from '@angular/core';
import { ProviderService } from 'app/services/provider.service';
import { Provider } from 'app/services/provider';
import { PagedResult } from 'app/paging/paged-result';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-list-providers',
  templateUrl: './list-providers.component.html',
  styleUrls: ['./list-providers.component.css'],
  providers: [ProviderService]
})
export class ListProvidersComponent implements OnInit {

  providers: Provider[];
  pagedResult: PagedResult<Provider>;
  pagingUrl: string;

  constructor(private providerService: ProviderService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.pagingUrl = '/providers';

        this.route.params.subscribe((params: Params) => {
            this.providerService.getProviders({
               page: params['page'] || 1,
               recordsPerPage: params['recordsPerPage'] || 10
            })
           .then((pagedResult: PagedResult<Provider>) => {
               this.pagedResult = pagedResult;
               this.providers = pagedResult.items;
           });
        });
  }
}
