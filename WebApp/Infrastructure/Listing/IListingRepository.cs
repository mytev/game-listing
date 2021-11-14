using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IListingRepository
    {
        Task<List<Listing>> GetAll();

        Task<Listing> Get(string id);

        Task Create(Listing listing);

        Task Remove(string listingId);

    }
}
