export interface AuctionCreate {
  name: string;
  description: string;
  images: File[];
  startPrice: number;
  finishInterval: number;
}
