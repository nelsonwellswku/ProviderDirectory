export class Provider{
    providerId?: string;
    npi: string;
    firstName: string;
    lastName: string;
    mailingAddress?: Address;
    practiceAddress?: Address;
}

class Address {
    streetOne: string;
    streetTwo: string;
    city: string;
    state: State;
    zip: string
}

class State {
    abbreviation: string;
    name: string;
}