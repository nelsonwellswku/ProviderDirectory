import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router'

import 'rxjs/add/operator/switchMap';

import { Provider } from './provider';
import { ProviderService } from './provider.service';

@Component({
    selector: 'provider-detail',
    templateUrl: 'app/provider-detail.component.html',
    providers: [ProviderService]
})
export class ProviderDetailComponent implements OnInit {
    provider : Provider;

    constructor(
        private providerService : ProviderService,
        private route: ActivatedRoute,
        private router : Router
    ) { 
        this.provider = new Provider();
    } 

    ngOnInit() : void {
        this.route.params
            .switchMap((params: Params) => this.providerService.getProvider(params['providerId']))
            .subscribe((provider : Provider) => this.provider = provider);
    }
}