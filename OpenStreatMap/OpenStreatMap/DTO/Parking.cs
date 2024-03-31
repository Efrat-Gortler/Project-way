using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using OpenStreatMap.DTO;

namespace OpenStreatMap.DTO
{
    public class Parking
    {
        public int CodeParking { get; set; }
        public int MaxPlace { get; set; }
        public int SeatTaken { get; set; }
       
        public string TypePrking { get; set; }
        public List<string> GateParking { get; set; }



        public int Availability()
        {
            return (MaxPlace - SeatTaken);


        }








    }
}
