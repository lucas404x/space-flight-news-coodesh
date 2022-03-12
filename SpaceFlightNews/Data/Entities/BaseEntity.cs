using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpaceFlightNews.Data.Entities 
{
    [BsonIgnoreExtraElements]
    public class BaseEntity 
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}