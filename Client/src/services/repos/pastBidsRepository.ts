import { PastBid} from "../../utils/types";
import {BaseRepository } from "../BaseRepository";

class PastBidsRepository extends BaseRepository<PastBid , null>{
    collection = "pastbids"

    getMany(){
        return super.getMany();
    }
   
}

export default PastBidsRepository;