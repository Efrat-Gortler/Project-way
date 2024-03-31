using MongoDB.Driver;
using OpenStreatMap.DTO;

namespace OpenStreatMap.Dal
{
    public class VehicleRepository
    {
        private readonly IMongoCollection<VehicleParking> _VehicleCollection;

        public VehicleRepository(IMongoCollection<VehicleParking> VehicleCollection)
        {
            _VehicleCollection = VehicleCollection;
        }

        public void InsertVehicle(VehicleParking VehicleParking)
        {
            _VehicleCollection.InsertOne(VehicleParking);
        }
        public void DeleteAllVehicleParking()
        {

            var filter = Builders<VehicleParking>.Filter.Empty;
            _VehicleCollection.DeleteMany(filter);


        }
    }
}
