export interface AuctionDto {
  id: number
  name: string
  description: string
  imageUrls: string[]
  score: number
  startDateTime: string
  endDateTime: string
  auctionistUserId: number
  auctionistUsername: string
  status: number
  isPaied: boolean
}
