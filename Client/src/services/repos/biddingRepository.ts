import { BidModel, BidSearchParameters, CreateBidRequest } from "../../utils/types";
import {BaseRepository } from "../BaseRepository";

class biddingRepository extends BaseRepository<BidModel | CreateBidRequest, BidSearchParameters>{
    collection = "bidding"

    getMany(){
        return super.getMany();
    }
    get(id: string){
        return super.get(id);
    }
    create(bid: CreateBidRequest){
        return super.create(bid)
    }
}

export default biddingRepository;