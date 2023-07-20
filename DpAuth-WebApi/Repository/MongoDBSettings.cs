using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DpAuthWebApi.Repository
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}