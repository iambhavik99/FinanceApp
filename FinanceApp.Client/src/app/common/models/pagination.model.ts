export class PaginationModel {
    pageSize!: number;
    pageIndex!: number;
    sortBy!: string;
    sortDirection: 'asc' | 'desc' | '' = 'asc';
}