import { Image } from '../../models/Images/image';

export interface AuctionDto {
  id: number
  name: string
  description: string
  images: Image[]
  score: number
  startDateTime: string
  endDateTime: string
  auctionistUserId: number
  auctionistUsername: string
  status: number
  isPaied: boolean
}
