import { Component, Input, OnInit } from '@angular/core';

import { PagedResult } from './paged-result';
import { Provider } from './provider';

@Component({
    selector: 'pages',
    templateUrl: 'app/paging.component.html'
})
export class PagingComponent<T> implements OnInit{

    pages : number[];

    @Input()
    url: string;

    @Input()
    pagedResult: PagedResult<T>;

    ngOnInit() : void {
        var pageCount = this.pagedResult.totalItems / this.pagedResult.currentRecordsPerPage;
        this.pages = new Array<number>();
        for(var i = 0; i < pageCount; i++) {
            this.pages.push(i + 1);
        }
    }
}