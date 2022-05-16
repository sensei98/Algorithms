namespace DijkstraStudent
{
    public class Edge
    {
        private Node a;
        private Node b;

        public Node A { get { return this.a; } }
        public Node B { get { return this.b; } }
        public int Weight { get; set; }

        public Edge(Node a, Node b, int weight)
        {
            this.a = a;
            this.b = b;
            this.Weight = weight;
        }
    }
}