export type Bid = {
    id: string,
    itemId: string,
    userId: string,
    ammount:number
}
export type BidSearchParameters = {
    id: string,
    itemId: string,
    userId: string,
    ammount:number
}
export type BidModel = {
    bidId: string,
    bidName: string,
    userId: string,
    highestBid: number
    startsAt:string;
    startsFrom:number
    endsAt:string
}

export type PlaceBidType = {
    bidId:string;
    ammount:number
}