export interface MyResponse<T> {
  currentPage: number;
  totalPages: number;
  data: T[];
}
