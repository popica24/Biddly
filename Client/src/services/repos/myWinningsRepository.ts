import { PastBid } from "../../utils/types";
import { BaseRepository } from "../BaseRepository";

class MyWinningsRepository extends BaseRepository<PastBid, null>{
    collection = "winnings"

    getMany(){
        return super.getMany();
    }
}

export default MyWinningsRepository;