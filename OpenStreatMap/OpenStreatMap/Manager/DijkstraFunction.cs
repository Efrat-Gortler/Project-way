//using MongoDB.Driver;

//using OpenStreetMap.Manager;
//using System;
//using System.Collections.Generic;
//using OpenStreatMap.Dal;
//using OpenStreatMap.DTO;

//namespace OpenStreetMap.Manager
//{
//    public class DijkstraFunction
//    {
//        private readonly NodeRepository _nodeRepository;

//        public DijkstraFunction(NodeRepository nodeRepository)
//        {
//            _nodeRepository = nodeRepository;
//        }

//        public void RunDijkstra(long startNodeId, Dictionary<long, double> distances)
//        {
//            List<Node> nodes = _nodeRepository.getGraph();
//            Node startNode = nodes.Find(n => n.NodeId == startNodeId);

//            if (startNode == null)
//            {
//                throw new ArgumentException("Start node not found");
//            }

//            foreach (var node in nodes)
//            {
//                node.Neighbors.ForEach(neighbor => neighbor.Weight = double.PositiveInfinity);
//                node.PreviousNode = null;
//            }

//            startNode.Neighbors.ForEach(neighbor => neighbor.Weight = 0);

//            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>(nodes);

//            while (priorityQueue.Count > 0)
//            {
//                Node currentNode = priorityQueue.ExtractMin();

//                foreach (var neighbor in currentNode.Neighbors)
//                {
//                    Node neighborNode = nodes.Find(n => n.NodeId ==neighbor.Id);

//                    if (neighborNode != null)
//                    {
//                        double newDistance = neighbor.Weight;

//                        if (newDistance < neighborNode.Neighbors[0].Weight) // Assuming only one neighbor in your case
//                        {
//                            neighborNode.Neighbors[0].Weight = newDistance;
//                            neighborNode.PreviousNode = currentNode;
//                            priorityQueue.UpdatePriority(neighborNode);
//                            distances[neighborNode.NodeId] = newDistance;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}




//using MongoDB.Driver;
//using OpenStreatMap.DTO;
//using OpenStreatMap.Dal;
//using OpenStreatMap.Manager;



//namespace OpenStreatMap.Manager
//{

//    public class DijkstraFunction
//    {
//        //    private readonly NodeRepository _nodeRepository;

//        //    public DijkstraFunction(NodeRepository nodeRepository)
//        //    {
//        //        _nodeRepository = nodeRepository;
//        //    }


//        //    public void RunDijkstra(int startNodeId, Dictionary<int, double> distances)

//        //    {

//        //        List<Node> nodes = _nodeRepository.getGraph();


//        //        Node startNode = nodes.Find(n => n.NodeId == startNodeId);

//        //        if (startNode == null)
//        //        {
//        //            throw new ArgumentException("Start node not found");
//        //        }

//        //        foreach (var node in nodes)
//        //        {
//        //            node.Weight = double.PositiveInfinity;
//        //            node.PreviousNode = null;
//        //        }

//        //        startNode.Weight = 0;

//        //        PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>(nodes);


//        //        while (priorityQueue.Count > 0)
//        //        {
//        //            Node currentNode = priorityQueue.ExtractMin();

//        //            foreach (var neighborId in currentNode.Neighbors)
//        //            {

//        //                var neighbor = nodes.Find(n => n.NodeId == (int)long.Parse(neighborId));

//        //                if (neighbor != null)
//        //                {
//        //                    Edge edge = new Edge(currentNode, neighbor);
//        //                    double newDistance = currentNode.Weight + edge.Weight;

//        //                    if (newDistance < neighbor.Weight)
//        //                    {
//        //                        neighbor.Weight = newDistance;
//        //                        neighbor.PreviousNode = currentNode;
//        //                        priorityQueue.UpdatePriority(neighbor);
//        //                        distances[neighbor.NodeId] = newDistance;
//        //                    }
//        //                }
//        //            }
//        //        }

//        //    }

//        //}
//    }

//    }
//// Now, nodes contain the shortest paths information, you can use it as needed







////private double GetEdgeWeight(Node node1, Node node2)
////{
////    var edge = new Edge(node1, node2);
////    return edge.Weight;
////}







