import { Component } from '@angular/core';

@Component({
    selector: 'site-navigation',
    template: `
        <ul>
            <li><a routerLink=''>Home</a></li>
            <li><a routerLink='createProvider'>Add a Provider</a></li>
        </ul>
    `
})
export class NavigationComponent { }