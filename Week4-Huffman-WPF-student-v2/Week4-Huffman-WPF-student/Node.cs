using System;
using System.Collections;
using System.Collections.Generic;

namespace Huffman
{
    /// <summary>
    /// With this class a tree can be build in which CharCount objects are stored.
    /// </summary>
    public class Node : IComparable , IEnumerable
    {
        public CharCount UserObject { get; set; }
        public Node LeftNode { get; private set; }
        public Node RightNode { get; private set; }

        public Node(CharCount cc)
        {
            this.UserObject = cc;
            this.LeftNode = null;
            this.RightNode = null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<Node> allNodes = new List<Node>();
            GetAllNodes(this, allNodes);

            foreach (Node node in allNodes)
                yield return node;
        }

        private void GetAllNodes(Node node, List<Node> nodes)
        {
            if (node == null) return;
            nodes.Add(node);
            GetAllNodes(node.LeftNode, nodes);
            GetAllNodes(node.RightNode, nodes);
        }

        public void AddNodeLeft(Node n)
        {
            LeftNode = n;
        }

        public void AddNodeRight(Node n)
        {
            RightNode = n;
        }

        public int CompareTo(object o)
        {
            Node k = (Node)o;
            if (k.UserObject.count < this.UserObject.count)
            {
                return 1;
            }
            else if (k.UserObject.count == this.UserObject.count)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int Count()
        {
            return UserObject.count;
        }
    }
}