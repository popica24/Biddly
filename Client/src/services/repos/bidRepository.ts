import { PlaceBidRequest } from "../../utils/types";
import {BaseRepository } from "../BaseRepository";

class bidRepository extends BaseRepository<PlaceBidRequest, null>{
    collection = "bid"

    create(placeBid: PlaceBidRequest){
        return super.create(placeBid);
    }

}

export default bidRepository;