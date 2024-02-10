export interface AuctionCreate {
  name: string;
  description: string;
  imageUrls: File[];
  startPrice: number;
  finishIntervalTicks: number;
}
