import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuctionDto } from "../../models/auction/auction-dto";
import { environment } from "../../../environments/environment";

@Injectable({
    providedIn: 'root'
})
export class AuctionService {
    constructor(private readonly httpClient: HttpClient) { }

    getAuctionById(id: string) {
        return this.httpClient.get<AuctionDto>(environment.apiUrl + 'auctions/' + id);
    }

    editAuction(id: string, data: FormData) {
        return this.httpClient.put(environment.apiUrl + 'auctions/' + id, data);
    }
    createAuction(data: FormData) {
        return this.httpClient.post(environment.apiUrl + 'auctions', data);
    }
}