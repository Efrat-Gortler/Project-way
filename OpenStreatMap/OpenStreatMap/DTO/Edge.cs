namespace OpenStreatMap.DTO
{
    public enum Enumdirection
    {

        Right, Left, TwoDirectional
    }
   
        
    
        public class Edge
        {
            public Node FirstNode { get; set; }
            public Node EndNode { get; set; }
            public double Weight { get; set; }
        public Enumdirection Direction { get; set; }

        public Edge(Node firstNode, Node endNode, Enumdirection direction)
            {
                FirstNode = firstNode;
                EndNode = endNode;
                Weight = Node.CalculateDistance(firstNode, endNode);
            Direction = direction;
        }
        }
    }




