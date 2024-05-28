import { Bid, BidModel, BidSearchParameters } from "../../utils/types";
import { BaseRepository } from "../BaseRepository";

class LatestBidsRepository extends BaseRepository<BidModel, BidSearchParameters>{
    collection = "latestbids"

    getMany(){
        return super.getMany();
    }
}

export default LatestBidsRepository;