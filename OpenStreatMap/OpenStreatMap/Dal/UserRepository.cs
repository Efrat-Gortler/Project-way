using MongoDB.Driver;
using OpenStreatMap.DTO;

namespace OpenStreatMap.Dal
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _UserCollection;

        public UserRepository(IMongoCollection<User> UserCollection)
        {
            _UserCollection = UserCollection;
        }

        public void InsertUser(User user)
        {
            _UserCollection.InsertOne(user);
        }
    }
}


