using System;

namespace Huffman
{
    /// <summary>
    /// In this class the binary code and the frequency for a letter is stored.
    /// </summary>
    public class CharCount : IComparable
    {
        public char character;
        public int count;
        public string binaryValue = "";

        public CharCount(char c)
        {
            character = c;
            count = 0;
        }

        public int CompareTo(object o)
        {
            CharCount c = (CharCount)o;
            if (c.count < this.count)
            {
                return 1;
            }
            else if (c.count == this.count)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}