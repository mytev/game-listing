using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ListingMongoRepository : IListingRepository
    {
        private readonly IMongoCollection<Listing> _listings;

        public ListingMongoRepository(IListingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this._listings = database.GetCollection<Listing>(settings.ListingsCollectionName);
        }

        public Task<List<Listing>> GetAll() =>
            _listings.Find(x => true).ToListAsync();

        public Task<Listing> Get(string id) =>
            _listings.Find(x => x.Id == id).FirstOrDefaultAsync();

        public Task Create(Listing listing) =>
            _listings.InsertOneAsync(listing);
        

        public Task Remove(string listingId) => _listings.DeleteOneAsync(book => book.Id == listingId);
    }
}
