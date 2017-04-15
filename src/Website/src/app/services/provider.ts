import { Taxonomy } from "app/services/taxonomy";
import { State } from "app/services/state";

export class Provider {
    providerId?: string;
    npi: string;
    firstName: string;
    lastName: string;
    primaryTaxonomy: Taxonomy;
    mailingAddress?: Address;
    practiceAddress?: Address;

    constructor() {
        this.mailingAddress = new Address();
        this.practiceAddress = new Address();
        this.primaryTaxonomy = new Taxonomy();
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
