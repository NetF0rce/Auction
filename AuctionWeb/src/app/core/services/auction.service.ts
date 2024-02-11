import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpInternalService } from "./http-internal.service";
import { MyResponse } from "../../models/common/response.model";
import { AuctionDto } from "../../models/auction/auction-dto";

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  constructor(private readonly httpClient: HttpInternalService) {
  }

  public getAuctions() {
    return this.httpClient.getRequest<MyResponse<AuctionDto>>(environment.apiUrl + 'auctions');
  }

  public getAuctionById(id: string) {
    return this.httpClient.getRequest<AuctionDto>(environment.apiUrl + 'auctions/' + id);
  }

  public editAuction(id: string, data: FormData) {
    return this.httpClient.putRequest<AuctionService>(environment.apiUrl + 'auctions/' + id, data);
  }

  public createAuction(data: FormData) {
    return this.httpClient.postRequest<AuctionDto>(environment.apiUrl + 'auctions', data);
  }
}
