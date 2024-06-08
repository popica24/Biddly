import { BaseRepository } from "../BaseRepository";

class WinnerRepository extends BaseRepository<string, null>{
    collection = "winner"

    get(id: string){
        return super.get(id);
    }
}

export default WinnerRepository;