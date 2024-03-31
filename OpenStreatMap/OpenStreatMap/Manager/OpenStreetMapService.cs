using OpenStreatMap.Dal;
using OpenStreatMap.DTO;
using System.Xml;

namespace OpenStreatMap.Manager
{

    // Replace "your_file_path.xml" with the actual path to your XML file

    // Load the XML document
    public class OpenStreetMapService
    {
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Implement your distance calculation logic (e.g., Haversine formula)
            // For simplicity, I'm providing a dummy distance calculation here
            return Math.Sqrt(Math.Pow(lat1 - lat2, 2) + Math.Pow(lon1 - lon2, 2));
        }
        public (List<Node>, List<Parking>) ProcessMapFile(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNodeList nodes = xmlDoc.SelectNodes("//node");
            List<Node> nodesNew = new List<Node>();
            List<Parking> parkingLots = new List<Parking>();

            using (XmlNodeList highwayWays = xmlDoc.SelectNodes("//way[tag[@k='highway']]"))
            {
                foreach (XmlNode way in highwayWays)
                {
                    Console.WriteLine($"Way ID: {way.Attributes["id"].Value}");

                    XmlNodeList refNodes = way.SelectNodes(".//nd[@ref]");

                    for (int i = 0; i < refNodes.Count - 1; i++)
                    {
                        string id = refNodes[i].Attributes["ref"].Value;
                        string targetNode = refNodes[i + 1].Attributes["ref"].Value;

                        // Check if the node with 'id' exists in the list
                     
                        Node sourceNode = nodesNew.FirstOrDefault(n => n.NodeId == id);
                      

                      

                        if (sourceNode == null)
                        {
                            // If not, create a new node
                            sourceNode = new Node { NodeId =id };
                            XmlNode xmlNode = nodes.Cast<XmlNode>().FirstOrDefault(n => n.Attributes["id"].Value == id); //לבדוק למה לא נכנס הlon lat בצורה נורמלית
                            if (xmlNode != null)
                            {
                                // Set the latitude and longitude properties
                                sourceNode.Lat = double.Parse(xmlNode.Attributes["lat"].Value);
                                sourceNode.Lon = double.Parse(xmlNode.Attributes["lon"].Value);
                            }
                           
                            nodesNew.Add(sourceNode);
                        }
                    



                        double weight = 0;

                        double lat1 = sourceNode.Lat; // Assuming Lat and Lon are the correct properties
                        double lon1 = sourceNode.Lon;
                        double lat2 = 0;
                        double lon2 = 0;

                        XmlNode node = nodes.Cast<XmlNode>().FirstOrDefault(n => n.Attributes["id"].Value == targetNode);
                        if (node != null)
                        {
                            lat2 = double.Parse(node.Attributes["lat"].Value);
                            lon2 = double.Parse(node.Attributes["lon"].Value);
                           
                        }



                        // Calculate distance using a custom method (you can use other formulas as needed)
                        double distance = CalculateDistance(lat1, lon1, lat2, lon2);
                        Neighbor neighbor = new Neighbor(targetNode, distance);
                        sourceNode.Neighbors.Add(neighbor);




                        //foreach (XmlNode node in nodes)
                        //{
                        //  if (node.Attributes["id"].Value == targetNode)
                        //   {

                        //       lat2 = double.Parse (node.Attributes["lat"].Value);
                        //        lon2 = double.Parse(node.Attributes["lon"].Value);

                        //   }


                        
                        //}







                        // Calculate distance using a custom method (you may use other formulas as needed)


                        // Calculate distance between nodes using latitude and longitude
                        //double lat1 = sourceNode.lat;
                        //double lon1 = sourceNode.lon;

                        //Node targetNodeObject = nodes.FirstOrDefault(n => n.NodeId == long.Parse(targetNode));
                        //if (targetNodeObject != null)
                        //{
                        //    double lat2 = targetNodeObject.lat;
                        //    double lon2 = targetNodeObject.lon;

                        //    // Calculate distance using a custom method (you may use other formulas as needed)
                        //    double distance = CalculateDistance(lat1, lon1, lat2, lon2);

                        //    // Update the Weight property with the calculated distance
                        //    sourceNode.Weight += distance;
                        //}


                        bool isParkingNode = way.SelectNodes("tag[@k='amenity' and @v='parking']").Count > 0;
                        if (isParkingNode)
                        {
                            // Set ParkingInfo for the node

                            sourceNode.TypNode = "parking";
                        }
                    }
                }
            }

          

            // Process parking lots
            XmlNodeList parkingWays = xmlDoc.SelectNodes("//way[tag[@k='amenity' and @v='parking']]");
            foreach (XmlNode parkingWay in parkingWays)
            {
                Parking parkingLot = new Parking
                {
                    CodeParking = parkingLots.Count + 1,
                    MaxPlace = GetMaxParkingCapacity(parkingWay),
                    SeatTaken = 0,
                    TypePrking = GetParkingType(parkingWay),
                    GateParking = new List<string>()

                };

                // Find and process gate nodes within the parking way
                XmlNodeList gateNodes = parkingWay.SelectNodes(".//nd[@ref]");
                foreach (XmlNode gateNode in gateNodes)
                {
                    string gateNodeId = gateNode.Attributes["ref"].Value;

                    //Node gate = nodesNew.FirstOrDefault(n => n.NodeId == long.Parse(gateNodeId));

                    // Check if the node is also part of a highway way
                    bool isOnHighway = IsNodeOnHighway(gateNode, xmlDoc);

                    if ( isOnHighway && !parkingLot.GateParking.Contains(gateNodeId))
                    {
                      
                        parkingLot.GateParking.Add(gateNodeId);
                    }
                }

                parkingLots.Add(parkingLot);
            }

            return (nodesNew, parkingLots);
        }
     
        private bool IsNodeOnHighway(XmlNode node, XmlDocument xmlDoc)
        {
            // Check if the node is part of a highway way
            string nodeId = node.Attributes["ref"].Value;
            return xmlDoc.SelectSingleNode($"//way[tag[@k='highway'] and nd[@ref='{nodeId}']]") != null;
        }

        private int GetMaxParkingCapacity(XmlNode parkingWay)
        {
            // Implement logic to get the maximum parking capacity from the parking way
            // For example, you can look for tags like "capacity" on the parking way
            // and parse the value. If not available, return a default value.
            return 50; // Default value for demonstration purposes
        }

        private string GetParkingType(XmlNode parkingWay)
        {
            // Implement logic to get the parking type from the parking way
            // For example, you can look for tags like "type" on the parking way.
            // If not available, return a default value.
            return "Surface Parking"; // Default value for demonstration purposes
        }

    }



    //class Program
    //  {
    //static void Main()
    //{
    //        string filePath = "file:///C:/Users/%D7%92%D7%95%D7%A8%D7%98%D7%9C%D7%A8/Downloads/P/ConsoleApp2/ConsoleApp2/bin/Debug/map.xml";

    //    OpenStreetMapService mapService = new OpenStreetMapService();
    //        List<Node> nodes = mapService.ProcessMapFile(filePath);

    //        // Replace 'your_osm_data.xml' with the path to your OSM XML file
    //        string filePath1 = "your_osm_data.xml";

    //    try
    //    {
    //        // Load the XML file
    //        XmlDocument doc = new XmlDocument();
    //        doc.Load(filePath);

    //        // Select all way elements with a "highway" tag
    //        XmlNodeList highwayWays = doc.SelectNodes("//way[tag[@k='highway']]");

    //        foreach (XmlNode way in highwayWays)
    //        {
    //            // Print information about the way
    //            Console.WriteLine($"Way ID: {way.Attributes["id"].Value}");

    //            // Iterate through nodes in the way
    //            foreach (XmlNode nd in way.SelectNodes("nd"))
    //            {
    //                string nodeId = nd.Attributes["ref"].Value;
    //                Console.WriteLine($"  Node ID: {nodeId}");

    //                // You can access more information about the node if needed
    //                // For example, node tags or coordinates
    //                XmlNode node = doc.SelectSingleNode($"//node[@id='{nodeId}']");
    //                Console.WriteLine($"    Latitude: {node.Attributes["lat"].Value}, Longitude: {node.Attributes["lon"].Value}");
    //            }

    //            Console.WriteLine();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Error: {ex.Message}");
    //    }
    //}
    //   }




    //static void BuildGraphFromOsmData(string osmData, out Dictionary<string, Tuple<double, double>> nodeLocations, out Dictionary<string, List<string>> graphG, out Dictionary<string, List<string>> graphT)
    //    {
    //        // Create dictionaries to store node locations and graph structure
    //        nodeLocations = new Dictionary<string, Tuple<double, double>>();
    //        graphG = new Dictionary<string, List<string>>();
    //        graphT = new Dictionary<string, List<string>>();

    //        // Load XML data
    //        string filePath = "file:///C:/Users/%D7%92%D7%95%D7%A8%D7%98%D7%9C%D7%A8/Downloads/P/ConsoleApp2/ConsoleApp2/bin/Debug/map.xml";

    //        // Load the XML document
    //        XmlDocument xmlDoc = new XmlDocument();
    //        List<string> selectedNodes = new List<string>();
    //        xmlDoc.Load(filePath);

    //        HashSet<string> nodeIdsToPrint = new HashSet<string>();

    //        // Add nodes to the graph
    //        foreach (XmlNode nodeElem in xmlDoc.SelectNodes(".//node"))
    //        {
    //            string nodeId = nodeElem.Attributes["id"].Value;
    //            double lat = Convert.ToDouble(nodeElem.Attributes["lat"].Value);
    //            double lon = Convert.ToDouble(nodeElem.Attributes["lon"].Value);
    //            nodeLocations[nodeId] = Tuple.Create(lat, lon);
    //            graphG[nodeId] = new List<string>();
    //            graphT[nodeId] = new List<string>();


    //        }

    //        // Add edges (arcs) to the graph
    //        foreach (XmlNode wayElem in xmlDoc.SelectNodes(".//way"))
    //        {
    //            XmlNodeList nodesRefs = wayElem.SelectNodes(".//nd");
    //            for (int i = 0; i < nodesRefs.Count - 1; i++)
    //            {
    //                string sourceNode = nodesRefs[i].Attributes["ref"].Value;
    //                string targetNode = nodesRefs[i + 1].Attributes["ref"].Value;
    //                graphG[sourceNode].Add(targetNode);
    //                graphT[sourceNode].Add(targetNode);

    //            }
    //        }
    //    }


    //    // Helper method to check if a node contains a specific label
    //    static bool ContainsLabel(XmlDocument xmlDoc, string nodeId, string label)
    //    {
    //        XmlNode nodeElem = xmlDoc.SelectSingleNode($".//node[@id='{nodeId}']");
    //        if (nodeElem != null)
    //        {
    //            XmlNode tagElem = nodeElem.SelectSingleNode($".//tag[@k='label' and @v='{label}']");
    //            return tagElem != null;
    //        }
    //        return false;
    //    }

    //    static Dictionary<string, double> Dijkstra(Dictionary<string, List<string>> graph, Dictionary<string, Tuple<double, double>> nodeLocations, string startNode)
    //    {
    //        var priorityQueue = new SortedSet<Tuple<double, string>>();
    //        var distances = new Dictionary<string, double>();

    //        // Initialize distances
    //        foreach (var node in graph.Keys)
    //        {
    //            distances[node] = double.MaxValue;
    //        }
    //        distances[startNode] = 0;

    //        priorityQueue.Add(Tuple.Create(0.0, startNode));

    //        while (priorityQueue.Count > 0)
    //        {
    //            var current = priorityQueue.Min;
    //            priorityQueue.Remove(current);

    //            double currentDistance = current.Item1;
    //            string currentNode = current.Item2;

    //            // If current distance is greater than the stored distance, skip
    //            if (currentDistance > distances[currentNode])
    //            {
    //                continue;
    //            }

    //            // Traverse neighbors of the current node
    //            foreach (var neighbor in graph[currentNode])
    //            {
    //                double distance = currentDistance + CalculateDistance(nodeLocations[currentNode], nodeLocations[neighbor]);

    //                // If a shorter path is found, update the distance
    //                if (distance < distances[neighbor])
    //                {
    //                    distances[neighbor] = distance;
    //                    priorityQueue.Add(Tuple.Create(distance, neighbor));
    //                }
    //            }
    //        }

    //        return distances;
    //    }

    //    static double CalculateDistance(Tuple<double, double> location1, Tuple<double, double> location2)
    //    {
    //        // Simple Euclidean distance formula
    //        double latDiff = location2.Item1 - location1.Item1;
    //        double lonDiff = location2.Item2 - location1.Item2;
    //        return Math.Sqrt(latDiff * latDiff + lonDiff * lonDiff) * 111000; // 1 degree of latitude is approximately 111,000 meters
    //    }

    //    static void CarEntersEdge(Dictionary<string, List<RoadSegment>> graph, string sourceNode, string targetNode, Dictionary<string, double> speedLimits)
    //    {
    //        // Search for the edge corresponding to the source and target nodes
    //        RoadSegment edgeToUpdate = graph[sourceNode].Find(e => e.TargetNode == targetNode);

    //        if (edgeToUpdate != null)
    //        {
    //            // Check if there is a speed limit defined for the road segment
    //            if (speedLimits.TryGetValue($"{sourceNode}-{targetNode}", out double roadSpeedLimit))
    //            {
    //                // Simulate a car entering the edge
    //                double roadLength = edgeToUpdate.Weight;

    //                // Update weight based on road speed limit and road length
    //                double updatedWeight = CalculateWeight(roadLength, roadSpeedLimit);
    //                edgeToUpdate.Weight = updatedWeight;

    //                Console.WriteLine($"Weight of edge {sourceNode} -> {targetNode} updated. New weight: {updatedWeight} meters");
    //            }
    //            else
    //            {
    //                Console.WriteLine($"Speed limit not found for edge {sourceNode} -> {targetNode}. Unable to update weight.");
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine($"Edge not found between {sourceNode} and {targetNode}");
    //        }
    //    }



}

