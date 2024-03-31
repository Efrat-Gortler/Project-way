// In DTO namespace
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class User
{
    //[BsonId]
    //[BsonRepresentation(BsonType.ObjectId)]
    //public string Id { get; set; }

    //[BsonElement("LicensePlate")]
    //public string LicensePlate { get; set; }

    //[BsonElement("Type")]
    //public string Type { get; set; }

    //[BsonElement("ParkingLotId")]
    //public string ParkingLotId { get; set; }

    //[BsonElement("EntryTime")]
    //public DateTime EntryTime { get; set; } = DateTime.UtcNow;

    //[BsonElement("EntryTime")]
    //public DateTime ExitTime { get; set; } = DateTime.UtcNow;
    [BsonElement("Destination")]
    public string Destination { get; set; }

    [BsonElement("TypePassenger")]
    public string TypePassenger { get; set; }

    [BsonElement("LicensePlateCar")]
    public string LicensePlateCar { get; set; }

}
