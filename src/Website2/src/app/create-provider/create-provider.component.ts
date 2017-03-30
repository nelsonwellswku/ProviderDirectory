import { Component, OnInit } from '@angular/core';
import { ProviderService } from 'app/services/provider.service';
import { StateService } from 'app/services/state.service';
import { CreateProviderForm } from 'app/services/create-provider-form';
import { State } from 'app/services/state';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-provider',
  templateUrl: './create-provider.component.html',
  styleUrls: ['./create-provider.component.css'],
  providers: [ProviderService, StateService]
})
export class CreateProviderComponent implements OnInit {

  provider: CreateProviderForm;
    states: State[];

    constructor(
        private providerService: ProviderService,
        private stateService: StateService,
        private router: Router
    ) {
        this.provider = new CreateProviderForm();
    }

    createProvider(form: CreateProviderForm): void {
        this.providerService.createProvider(form)
            .then(x => this.router.navigate(['providers', x]));
    }

    ngOnInit() {
        this.stateService.getStates()
        .then(states => this.states = states);
    }
}
