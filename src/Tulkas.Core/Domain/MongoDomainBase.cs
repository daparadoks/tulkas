using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tulkas.Core.Domain
{
    public class MongoDomainBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}