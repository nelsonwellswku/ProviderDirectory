import { Component, OnInit, Input } from '@angular/core';
import { PagedResult } from 'app/paging/paged-result';

@Component({
  selector: 'app-pages',
  templateUrl: './paging.component.html',
  styleUrls: ['./paging.component.css']
})
export class PagingComponent<T> {

    pages: number[];
    recordsPerPage: number;
    currentPage: number;

    @Input()
    url: string;

    _pagedResult: PagedResult<T>;

    @Input()
    set pagedResult(pagedResult: PagedResult<T>) {
        this._pagedResult = pagedResult;
        this.setPagingValues();
    }

    get pagedResult(): PagedResult<T> { return this._pagedResult; }

    private setPagingValues(): void {
        this.currentPage = this._pagedResult.currentPage;
        this.recordsPerPage = this._pagedResult.currentRecordsPerPage;

        const pageCount = this._pagedResult.totalItems / this._pagedResult.currentRecordsPerPage;
        this.pages = new Array<number>();
        for (let i = 0; i < pageCount; i++) {
            this.pages.push(i + 1);
        }
    }

}
