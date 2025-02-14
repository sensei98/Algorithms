﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DijkstraStudent
{
    // declare a delegate type for displaying a graph
    public delegate void DisplayGraphDelegate(Node startNode, Node visitedNode);

    public partial class DijkstraForm : Form
    {
        private Graph graph;
        private List<Node> visitedNodes = new List<Node>();

        public DijkstraForm()
        {
            InitializeComponent();

            InitGraph();
            InitListView();
        }

        private void InitGraph()
        {
            this.graph = new Graph();

            List<Node> nodes = this.graph.Nodes;
            List<Edge> edges = this.graph.Edges;

            // add nodes
            Node a = new Node("A"); nodes.Add(a);
            Node b = new Node("B"); nodes.Add(b);
            Node c = new Node("C"); nodes.Add(c);
            Node d = new Node("D"); nodes.Add(d);
            Node e = new Node("E"); nodes.Add(e);
            Node f = new Node("F"); nodes.Add(f);
            Node g = new Node("G"); nodes.Add(g);

            // add edges
            edges.Add(new Edge(a, b, 4));
            edges.Add(new Edge(a, c, 3));
            edges.Add(new Edge(a, e, 7));
            edges.Add(new Edge(b, c, 6));
            edges.Add(new Edge(b, d, 5));
            edges.Add(new Edge(c, d, 11));
            edges.Add(new Edge(c, e, 8));
            edges.Add(new Edge(d, e, 2));
            edges.Add(new Edge(d, f, 2));
            edges.Add(new Edge(d, g, 10));
            edges.Add(new Edge(e, g, 5));
            edges.Add(new Edge(f, g, 3));
        }

        private void InitListView()
        {
            // add columns
            foreach (Node node in graph.Nodes)
            {
                ColumnHeader header = lvResults.Columns.Add(node.Name);
                header.TextAlign = HorizontalAlignment.Right;

            }
        }

        private void DisplayResults(Node startingNode, Node visitedNode)
        {
            visitedNodes.Add(visitedNode);

            // add row
            ListViewItem lvItem = new ListViewItem(startingNode.Name);
            lvItem.UseItemStyleForSubItems = false;
            foreach (Node n in graph.Nodes)
            {
                if (n == visitedNode)
                {
                    ListViewItem.ListViewSubItem subItem = lvItem.SubItems.Add($"{n.Distance}");
                    subItem.ForeColor = Color.Red;
                }
                else if (visitedNodes.Contains(n))
                {
                    ListViewItem.ListViewSubItem subItem = lvItem.SubItems.Add($"{n.Distance}");
                    subItem.ForeColor = Color.LightGray;
                }
                else if (n.Distance == int.MaxValue)
                    lvItem.SubItems.Add("∞");
                else
                    lvItem.SubItems.Add($"{n.Distance}");
            }
            lvResults.Items.Add(lvItem);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();

            // create (Dijkstra) algorithm object
            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();
            dijkstra.DisplayGraph = DisplayResults;

            // take each node as starting node
            foreach (Node node in this.graph.Nodes)
            {
                this.visitedNodes.Clear();

                // calculate from (starting) node shortest path to all other nodes
                dijkstra.CalculateShortestPaths(this.graph, node);

                // add empty line in listview (to separate results of starting nodes)
                lvResults.Items.Add("");
            }
        }
    }
}