import { Component, OnInit } from '@angular/core';
import { ProviderService } from 'app/services/provider.service';
import { StateService } from 'app/services/state.service';
import { State } from 'app/services/state';
import { Router } from '@angular/router';
import { CreateProviderCommand } from "app/services/create-provider-command";

@Component({
  selector: 'app-create-provider',
  templateUrl: './create-provider.component.html',
  styleUrls: ['./create-provider.component.css'],
  providers: [ProviderService, StateService]
})
export class CreateProviderComponent implements OnInit {

  provider: CreateProviderCommand;
    states: State[];

    constructor(
        private providerService: ProviderService,
        private stateService: StateService,
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
    }
}
