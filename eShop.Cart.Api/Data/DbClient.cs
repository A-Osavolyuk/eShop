namespace eShop.Cart.Api.Data;

public class DbClient
{
    public DbClient(IOptions<MongoDbSettings> settings)
    {
        var connectionString = settings.Value.ConnectionString;
        var databaseName = settings.Value.DatabaseName;
        var client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
    }

    private readonly IMongoDatabase database;

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        if (string.IsNullOrEmpty(collectionName))
        {
            throw new MongoException("Cannot get collection with empty or null collection name");
        }

        return database.GetCollection<T>(collectionName);
    }
}