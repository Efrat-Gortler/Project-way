using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OpenStreatMap.DTO
{
    public class VehicleParking
    {

        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string IdVehicle { get; set; }

        [BsonElement("EntryTime")]

        public DateTime AtTime { get; set; } = DateTime.UtcNow;

        
        public bool TransitionType { get; set; }
        public string GateNumber { get; set; }











    }

}

