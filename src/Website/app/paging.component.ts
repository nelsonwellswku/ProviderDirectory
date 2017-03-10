import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

import { PagedResult } from './paged-result';
import { Provider } from './feature/provider';

@Component({
    selector: 'pages',
    templateUrl: 'app/paging.component.html'
})
export class PagingComponent<T> implements OnChanges {

    pages: number[];
    recordsPerPage: number;
    currentPage: number;

    @Input()
    url: string;

    @Input()
    pagedResult: PagedResult<T>;

    ngOnChanges(changes: SimpleChanges): void {
        for (let propName in changes) {
            if (propName == "pagedResult") {
                let changedProp = changes[propName];
                this.pagedResult = changedProp.currentValue;
                this.setPagingValues();
            }
        }
    }

    private setPagingValues(): void {
        this.currentPage = this.pagedResult.currentPage;
        this.recordsPerPage = this.pagedResult.currentRecordsPerPage;

        var pageCount = this.pagedResult.totalItems / this.pagedResult.currentRecordsPerPage;
        this.pages = new Array<number>();
        for (var i = 0; i < pageCount; i++) {
            this.pages.push(i + 1);
        }
    }
}