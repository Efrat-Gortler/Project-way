namespace OpenStreatMap.DTO
{
    public class Neighbor
    {
        public string id { get; set; }
        public double weight { get; set; }

        public Neighbor(string id, double weight)
        {
            this.id = id;
            this.weight = weight;
        }
    }
}
