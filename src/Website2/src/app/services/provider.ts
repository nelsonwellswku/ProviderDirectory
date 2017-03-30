import { State } from './state';

export class Provider {
    providerId?: string;
    npi: string;
    firstName: string;
    lastName: string;
    mailingAddress?: Address;
    practiceAddress?: Address;

    constructor() {
        this.mailingAddress = new Address();
        this.practiceAddress = new Address();
    }
}

class Address {
    streetOne: string;
    streetTwo: string;
    city: string;
    state: State;
    zip: string;

    constructor() {
        this.state = new State();
    }
}
