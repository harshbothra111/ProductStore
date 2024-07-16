export interface PaginationResponse<T> {
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalRecords: number;
    data: T[];
    hasPrevious: boolean;
    hasNext: boolean;
}