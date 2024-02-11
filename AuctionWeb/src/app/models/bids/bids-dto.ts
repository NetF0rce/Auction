export interface BidsDto {
  id: number,
  auctionId: number,
  bidderId: number,
  amount: number,
  isWinner: boolean,
  dateAndTime: Date,
  bidderName: string;
}
