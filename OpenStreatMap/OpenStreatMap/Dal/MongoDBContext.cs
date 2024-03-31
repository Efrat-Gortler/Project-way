using MongoDB.Driver;
using OpenStreatMap.DTO;


namespace OpenStreatMap.Dal
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;
       


        public MongoDBContext()
        {
            string connectionString = "mongodb+srv://efrat6200:mongoDB123@cluster0.mwidfdb.mongodb.net/?retryWrites=true&w=majority"

                , databaseName = "GraphOSM";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Node> NodesGraph => _database.GetCollection<Node>("Node");
                public IMongoCollection<User> User => _database.GetCollection<User>("User");
        public IMongoCollection<VehicleParking> VehicleParking => _database.GetCollection<VehicleParking>("VehicleParking");
        public IMongoCollection<Parking> Parkings => _database.GetCollection<Parking>("Parking");
        //public IMongoCollection<DTO.Node> NodesGraph => _database.GetCollection<DTO.Node>("NodesGraph");//גיפיטי אמר לעשות


    }
}


