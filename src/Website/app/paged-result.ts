export class PagedResult<T> {
    items: T[]
    totalItems: number
    currentPage: number;
    currentRecordsPerPage: number;
}