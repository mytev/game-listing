namespace Infrastructure
{
    public class ListingDatabaseSettings : IListingDatabaseSettings
    {
        public string ListingsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
