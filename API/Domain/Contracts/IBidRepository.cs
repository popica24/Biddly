using Business.Domain;
using Business.Reponses;

namespace Business.Contracts;

public interface IBidRepository
{
    InternalResponse<bool> Create(Bid bid);
     InternalResponse<Bid> Get { get; set; }
     InternalResponse<Bid[]> Search { get; set; }
}
