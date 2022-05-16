using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Huffman
{
    /// <summary>
    /// Class that implements the Huffman encoding algorithm.
    /// </summary>
    public class Huffman
    {
        private const int MAXCHAR = 256;    // max 256 different characters in the ASCII table

        private CharCount[] charCount;      // array with a CharCount object for each character
        private Node tree;                  // the tree with the (binary) codes for all characters in the message

        public Huffman()
        {
            // for each letter/character the corresponding binary value and frequency is stored
            charCount = new CharCount[MAXCHAR];
            for (int c = 0; c < MAXCHAR; c++)
                charCount[c] = new CharCount((char)c);
        }

        public string Encode(string input, ListBox frequenciesList)
        {
            // 1. process each letter in the input string and increase the corresponding count in (array) charCount

            foreach(char c in input)
            {
                charCount[c].count++;
            }

            // 2. create nodes for all relevant charCount objects, with the charCount as userObject
            // and put these nodes in a new ArrayList (size of an ArrayList can be resized/shrinked, which is needed later)
            ArrayList nodes = new ArrayList();
            for (int i = 0; i < MAXCHAR; i++) 
            {
                if(charCount[i].count > 0)
                {
                    Node newNode = new Node(charCount[i]);
                    nodes.Add(newNode);
                }
               
            }

            // 3. now repeat (while there are at least 2 nodes in the sorted ArrayList):
            // create a new node containing the 2 nodes with lowest count as children
            // replace these 2 nodes with the new node in the ArrayList (with letter 0 as char, and the sum of the 2 counts as count)
            // make sure the new node is put in the right position in the Array list
            // every loop iteration the size of the ArrayList should decrease by 1
            // ...
            nodes.Sort();
            do
            {
                Node node = new Node(new CharCount('0'));
                node.AddNodeLeft((Node)nodes[0]);
                node.AddNodeRight((Node)nodes[1]);
                node.UserObject.count = ((Node)nodes[0]).Count() + ((Node)nodes[1]).Count();
                nodes[1] = node;
                nodes.RemoveAt(0);
                nodes.Sort();
            } while (nodes.Count >= 2);


            

            // 4. now give all leaves in the tree their corresponding binary value
            // this can be done with a recursive method with parameter the "current" binary value and
            // by adding an extra '0' every time you go left, and by adding an extra '1' every time you go right
            // when both left side and right side are null, you have reached a leave and you can assign the binary value to it

            tree = (Node)nodes[0];
            AssignBinaryValue(tree, "");
            

            // the tree has now been created

            // 5. process each character in the input string
            // search the node that belongs to the current character
            // (use a foreach to iterate through all tree nodes)
            // add the binary value of the node to the output string

            string outputStr = "";
            foreach(char c in input)
            {
                foreach(Node node in tree)
                {
                    if(node.UserObject.character == c) outputStr += node.UserObject.binaryValue; 
                }
            }

            // 6. iterate all nodes in the tree
            // add new items to frequenciesList.Items with format: "count x character"

            foreach(Node node in tree)
            {
                if(node.UserObject.character != '0') frequenciesList.Items.Add($"{node.UserObject.count} x {node.UserObject.character}");
            }
            // return the output string
            return outputStr;
        }
        public void AssignBinaryValue(Node node, string current)
        {
            if(node.UserObject.character == '0')
            {
                if (node.LeftNode != null) AssignBinaryValue(node.LeftNode, (current + '0'));
                if (node.RightNode != null) AssignBinaryValue(node.RightNode, (current + '1')); 
            }
            else node.UserObject.binaryValue = current;
           
        }

        public string Decode(string input)
        {
            string str = "";

            // process the input string (containing 0's and 1's)
            // start at the root of the tree, a 0 means go left, a 1 means go right
            // when a leaf has been reached, then add the corresponding character
            // to the output and restart from the root of the tree

            while(input.Length > 0)
            {
                Tuple<char,int> tuple = decodeBinary(tree, input);
                str += tuple.Item1;
                input = input.Substring(tuple.Item2);
            }

            //do
            //{
            //    Dictionary<char, int> dict = decodeBinaryValue(tree, input);
            //    str += dict.Keys;
            //    input += dict.Values;

            //} while (input.Length > 0);

            // return output string
            return str;
        }
        public Tuple<char,int> decodeBinary(Node node, string encodedText, int count = 0)
        {
            Tuple<char, int> tuple; // matching character with corresponsing integer(0,1)
            if (node.UserObject.character == '\0')
            {
                if(encodedText[count] == '0')
                {
                    tuple = decodeBinary(node.LeftNode, encodedText, count + 1);
                }
                else
                {
                    tuple = decodeBinary(node.RightNode, encodedText, count + 1);
                }

            }
            else
            {
                tuple = new Tuple<char, int>(node.UserObject.character, count); 
            }
           
            return tuple;
        }
        //public Dictionary<char,int> decodeBinaryValue(Node node, string text, int count = 0)
        //{
        //    Dictionary<char, int> dict;
        //    if(node.UserObject.character == '0')
        //    {
        //        if(text[count] == '0')
        //        {
        //            dict = decodeBinaryValue(node.LeftNode, text, count + 1);

        //        }
        //        else
        //        {
        //            dict = decodeBinaryValue(node.RightNode, text, count + 1); 
        //        }
        //    }
        //    else
        //    {
        //        //dict = new Dictionary<char, int>(node.UserObject.character, count);
        //    }
        //    return dict;
        //}
        

     
    }
}