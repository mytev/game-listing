namespace Infrastructure
{
    public interface IListingDatabaseSettings
    {
        string ListingsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
