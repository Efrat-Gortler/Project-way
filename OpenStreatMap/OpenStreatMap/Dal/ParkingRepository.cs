using MongoDB.Driver;
using OpenStreatMap.DTO;

namespace OpenStreatMap.Dal
{
    public class ParkingRepository
    {
        private readonly IMongoCollection<Parking> _parkingsCollection;

        public ParkingRepository(IMongoCollection<Parking> parkingsCollection)
        {
            _parkingsCollection = parkingsCollection;
        }


        public void InsertParkings(List<Parking> parkings)
        {
            foreach (var parking in parkings)
            {
                _parkingsCollection.InsertOne(parking);
            }
        }
    }
}
