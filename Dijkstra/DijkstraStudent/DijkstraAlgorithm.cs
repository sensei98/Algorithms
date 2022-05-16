using System.Collections.Generic;
using System.Linq;

namespace DijkstraStudent
{
    public class DijkstraAlgorithm
    {
        public DisplayGraphDelegate DisplayGraph { get; set; }

        public DijkstraAlgorithm()
        {
            // ...
        }

        public void CalculateShortestPaths(Graph graph, Node startingNode)
        {
            List<Node> unvisitedNodes = new List<Node>();
            // step 1...
            foreach (Node node in graph.Nodes)
            {
                node.Distance = int.MaxValue;
                unvisitedNodes.Add(node);
            }
            startingNode.Distance = 0;

            // step 2...

            // continue the algorithm, while there are more nodes to check
            while (unvisitedNodes.Count > 0)
            {
                Node currentNode = unvisitedNodes.OrderBy(n => n.Distance).First();
               
                // step 3...
                var neighbours = graph.Edges.Where(n => n.A == currentNode || n.B == currentNode);
                var NeighbourNodes = neighbours.Select(nn => nn.B == currentNode ? nn.A : nn.B);

                unvisitedNodes.Remove(currentNode);

                // step 4...
                foreach (var neighbour in NeighbourNodes)
                {
                    Edge edge = neighbours.Single(e => e.A == currentNode && e.B == neighbour ||
                    e.B == currentNode && e.A == neighbour);

                    int distance = currentNode.Distance + edge.Weight;

                    if (neighbour.Distance > distance) neighbour.Distance = distance;
                    neighbour.ParentNode = currentNode;
                }

                //update interface
                DisplayGraph(startingNode, currentNode);
            }
        }
    }
}