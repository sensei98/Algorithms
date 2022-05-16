using System.Collections.Generic;

namespace DijkstraStudent
{
    public class Graph
    {
        // a graph consists of a set of nodes and a set of edges (between nodes)
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();

        public List<Node> Nodes { get { return this.nodes; } }
        public List<Edge> Edges { get { return this.edges; } }

        public Graph()
        {
            // ...
        }

        public void AddNode(Node node)
        {
            this.nodes.Add(node);
        }

        public void AddEdge(Edge edge)
        {
            this.edges.Add(edge);
        }
    }
}