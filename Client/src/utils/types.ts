export type BidSearchParameters = {
    id: string,
    itemId: string,
    userId: string,
    ammount:number
}
export type BidModel = {
    bidId: string;
    createdBy: string;
    itemName: string;
    wonBy: string;
    wonAt: string;
    startingAt:string;
    highestBid: number;
    startingFrom: number;
    pastBid:boolean;
    username?:string
}

export type PastBid = {
    bidId:string;
    itemName:string;
    wonAt:string;
    highestBid:number;
    username: string;
}

export type PlaceBidRequest = {
    bidId: string;
    bidderId: string;
    ammount: number
}

export type User = {
    id: string,
    username:string,
    email:string
}

export type CreateBidRequest = {
    createdBy:string,
    startingFrom: number,
    itemName: string,
    startingAt: string,
    wonAt: string
}