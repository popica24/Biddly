import { PlaceBidType } from "../../utils/types";
import {BaseRepository } from "../BaseRepository";

class biddingRepository extends BaseRepository<PlaceBidType, null>{
    collection = "bid"

    create(placeBid: PlaceBidType){
        return super.create(placeBid);
    }

}

export default biddingRepository;