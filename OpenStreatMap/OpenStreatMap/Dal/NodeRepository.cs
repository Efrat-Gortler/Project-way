using MongoDB.Driver;
using OpenStreatMap.DTO;
namespace OpenStreatMap.Dal
{
    public class NodeRepository
    {
        private readonly IMongoCollection<Node> _nodesCollection;

        
        public NodeRepository(IMongoCollection<Node> nodesCollection)
        {
            _nodesCollection = nodesCollection;
          

        }
     


        public void InsertNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                _nodesCollection.InsertOne(node);
            }
        }


        public List<Node> getGraph()
        {
            return _nodesCollection.Find(Node => true).ToList();
        }

        public void DeleteAllNodes()
        {
           
                var filter = Builders<Node>.Filter.Empty;
                _nodesCollection.DeleteMany(filter);
            
         
        }

    }

   

  
}
