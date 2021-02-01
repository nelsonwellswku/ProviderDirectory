import { Component, OnInit } from '@angular/core';
import { ProviderService } from 'app/services/provider.service';
import { StateService } from 'app/services/state.service';
import { State } from 'app/services/state';
import { Router } from '@angular/router';
import { CreateProviderCommand } from 'app/services/create-provider-command';
import { TaxonomyService } from 'app/services/taxonomy.service';
import { Taxonomy } from 'app/services/taxonomy';
import { PagedResult } from 'app/paging/paged-result';

@Component({
    selector: 'app-create-provider',
    templateUrl: './create-provider.component.html',
    styleUrls: ['./create-provider.component.css'],
    providers: [ProviderService, StateService, TaxonomyService]
})
export class CreateProviderComponent implements OnInit {

    provider: CreateProviderCommand;
    states: State[];
    taxonomies: Taxonomy[];

    constructor(
        private providerService: ProviderService,
        private stateService: StateService,
        private taxonomyService: TaxonomyService,
        private router: Router
    ) {
        this.provider = new CreateProviderCommand();
    }

    createProvider(form: CreateProviderCommand): void {
        this.providerService.createProvider(form)
            .then(x => this.router.navigate(['providers', x]));
    }

    ngOnInit() {
        this.stateService.getStates()
            .then(states => this.states = states);

        this.taxonomyService.getTaxonomies({ page: 1, recordsPerPage: 1000 })
            .then((result: PagedResult<Taxonomy>) => this.taxonomies = result.items);
    }
}
