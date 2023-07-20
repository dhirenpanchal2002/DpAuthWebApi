using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DpAuthWebApi.Repository
{
    public interface IMongoDBSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
