import { BidModel, BidSearchParameters } from "../../utils/types";
import {BaseRepository } from "../BaseRepository";

class bidRepository extends BaseRepository<BidModel, BidSearchParameters>{
    collection = "bidding"

    getMany(){
        return super.getMany();
    }
    get(id: string){
        return super.get(id);
    }
}

export default bidRepository;