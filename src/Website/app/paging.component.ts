import { Component, Input } from '@angular/core';

import { PagedResult } from './paged-result';
import { Provider } from './feature/provider';

@Component({
    selector: 'pages',
    templateUrl: 'app/paging.component.html'
})
export class PagingComponent<T> {

    pages: number[];
    recordsPerPage: number;
    currentPage: number;

    @Input()
    url: string;

    _pagedResult : PagedResult<T>;

    @Input()
    set pagedResult(pagedResult : PagedResult<T>) {
        this._pagedResult = pagedResult;
        this.setPagingValues();
    }

    get pagedResult(): PagedResult<T> { return this._pagedResult; }

    private setPagingValues(): void {
        this.currentPage = this._pagedResult.currentPage;
        this.recordsPerPage = this._pagedResult.currentRecordsPerPage;

        var pageCount = this._pagedResult.totalItems / this._pagedResult.currentRecordsPerPage;
        this.pages = new Array<number>();
        for (var i = 0; i < pageCount; i++) {
            this.pages.push(i + 1);
        }
    }
}