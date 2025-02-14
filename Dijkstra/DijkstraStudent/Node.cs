﻿using System;

namespace DijkstraStudent
{
    public class Node : IComparable<Node>
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public Node ParentNode { get; set; }

        public Node(string name)
        {
            Name = name;
        }

        public int CompareTo(Node other)
        {
            if (this.Distance < other.Distance) return -1;  // 'this' object preceeds 'other' object
            else if (this.Distance > other.Distance) return 1;
            else return 0;
        }
    }
}