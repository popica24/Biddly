import { User } from "../../utils/types";
import { BaseRepository } from "../BaseRepository";

class UserRepository extends BaseRepository<User, null>{
    collection = "user"

    getMany(){
        return super.getMany();
    }
}

export default UserRepository;