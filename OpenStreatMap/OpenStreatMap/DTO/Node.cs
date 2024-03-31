using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace OpenStreatMap.DTO

{
    [BsonIgnoreExtraElements]
    public class Node : Point, IComparable<Node>
    {
        public int CompareTo(Node other)
        {
            // Implement your comparison logic here
            // For example, you might compare based on the Weight property
            return this.NodeId.CompareTo(other.NodeId);
        }
        [BsonId]
       
        public string NodeId { get; set; }

        [BsonElement("type")]
        public string type { get; set; } = string.Empty;

        //[BsonElement("Weight")]
        //public double Weight { get; set; }

        [BsonElement("Neighbors")]
        public List<Neighbor> Neighbors { get; set; } = new List<Neighbor>();

        [BsonElement("TypNode")]
        public string TypNode { get; set; } = string.Empty;

        public double Lat { get; set; }
        public double Lon { get; set; }
        public Node PreviousNode { get; set; }

        public static double CalculateDistance(Node node1, Node node2)
        {
            // Replace this with your distance calculation formula based on the latitudes and longitudes of the nodes
            // Example: using Haversine formula
            double lat1 = node1.Lat;
            double lon1 = node1.Lon;
            double lat2 = node2.Lat;
            double lon2 = node2.Lon;

            double R = 6371; // Earth radius in kilometers

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }





    }
}
