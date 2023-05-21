using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_lib_cs
{
    /// <summary>
    /// Class for performing Huffman encoding and decoding.
    /// </summary>
    public class HuffmanAlgorithm
    {
        
        /// Node class for text-based Huffman encoding.
        public class Node_Txt
        {
            // Symbol represented by the node
            public char Symbol { get; set; }

            // Frequency of the symbol
            public int Frequency { get; set; }

            // Right child node
            public Node_Txt Right { get; set; }

            // Left child node
            public Node_Txt Left { get; set; }

            // Traverse the Huffman tree to find the encoded data for a symbol
            public List<bool> Traverse(char symbol, List<bool> data)
            {
                // If the symbol matches the node's symbol, return the encoded data
                if (Right == null && Left == null)
                {
                    if (symbol.Equals(this.Symbol))
                    {
                        return data;
                    }
                    else
                    {
                        // Symbol not found in the current node
                        return null;
                    }
                }
                else
                {
                
                    List<bool> left = null;
                    List<bool> right = null;

                    // Traverse the left subtree
                    if (Left != null)
                    {
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);
                        left = Left.Traverse(symbol, leftPath);
                    }

                    // Traverse the right subtree
                    if (Right != null)
                    {
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);
                        right = Right.Traverse(symbol, rightPath);
                    }

                    // If the symbol is found in the left subtree, return the encoded data from the left path
                    if (left != null)
                    {
                        return left;
                    }
                    else
                    {
                        // If the symbol is found in the right subtree, return the encoded data from the right path
                        return right;
                    }
                }
            }
        }

        // Huffman tree class for text-based Huffman encoding
        public class HuffmanTree
        {
            // List of nodes in the Huffman tree
            private List<Node_Txt> nodes = new List<Node_Txt>();

            // Root node of the Huffman tree
            public Node_Txt Root { get; set; }

            // Frequency of each symbol
            public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

            // Build the Huffman tree from the given source string
            public void Build(string source)
            {
                // Calculate the frequency of each symbol in the source string
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                // Create leaf nodes for each symbol and add them to the list of nodes
                foreach (KeyValuePair<char, int> symbol in Frequencies)
                {
                    nodes.Add(new Node_Txt()
                    {
                        Symbol = symbol.Key,
                        Frequency = symbol.Value
                    });
                }

                // Build the Huffman tree by combining nodes with the lowest frequencies
                while (nodes.Count > 1)
                {
                    List<Node_Txt> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node_Txt>();

                    if (orderedNodes.Count >= 2)
                    {
                        List<Node_Txt> taken = orderedNodes.Take(2).ToList<Node_Txt>();
                        Node_Txt parent = new Node_Txt()
                        {
                            Symbol = '*', 
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };
                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    // Set the root of the Huffman tree as the remaining node in the list
                    this.Root = nodes.FirstOrDefault(); 
                }
            }

            // Encode the source string using the built Huffman tree
            public BitArray Encode(string source)
            {
                List<bool> encodedSource = new List<bool>();

                // Traverse the Huffman tree for each symbol in the source string and append the encoded data
                for (int i = 0; i < source.Length; i++)
                {
                    List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>()); 
                    encodedSource.AddRange(encodedSymbol);
                }

                // Convert the list of bools to a BitArray and return it
                BitArray bits = new BitArray(encodedSource.ToArray()); 
                return bits;
            }

            // Decode the given BitArray using the built Huffman tree and return the decoded string
            public string Decode(BitArray bits)
            {
                Node_Txt current = this.Root;
                string decoded = "";

                // Traverse the Huffman tree based on each bit in the BitArray and decode the symbols
                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    // If a leaf node is reached, append the symbol to the decoded string and reset the current node
                    if (IsLeaf(current))
                    {
                        decoded += current.Symbol;
                        current = this.Root;
                    }
                }

                return decoded;
            }


            // Check if the given node is a leaf node (has no children)
            public bool IsLeaf(Node_Txt node)
            {
                return (node.Left == null && node.Right == null);
            }
        }

        // Node class for MP3-based Huffman encoding
        public class Node_mp3
        {
            // Symbol represented by the node (byte value)
            public byte Symbol { get; set; }

            // Frequency of the symbol
            public int Frequency { get; set; }

            // Left child node
            public Node_mp3 Left { get; set; }

            // Right child node
            public Node_mp3 Right { get; set; }


            // Traverse the Huffman tree to find the encoded data for a symbol
            public List<bool> Traverse_mp3(byte? symbol, List<bool> data)
            {
                if (Left == null && Right == null)
                {
                    // If the symbol matches the node's symbol, return the encoded data
                    if (Symbol == symbol)
                    {
                        return data;
                    }
                    else
                    {
                        // Symbol not found in the current node
                        return null;
                    }
                }
                else
                {
                    
                    List<bool> left = null;
                    List<bool> right = null;

                    // Traverse the left subtree
                    if (Left != null)
                    {
                        
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);
                        left = Left.Traverse_mp3(symbol, leftPath);
                    }

                    // Traverse the right subtree
                    if (Right != null)
                    {
                        
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);
                        right = Right.Traverse_mp3(symbol, rightPath);
                    }

                    // If the symbol is found in the left subtree, return the encoded data from the left traversal
                    if (left != null)
                    {
                        
                        return left;
                    }
                    else
                    {
                        // Otherwise, return the encoded data from the right traversal
                        return right;
                    }
                }
            }
        }

        // Huffman tree class for MP3-based encoding
        public class HuffmanTree_mp3
        {
            // List of nodes in the Huffman tree
            private List<Node_mp3> nodes = new List<Node_mp3>();

            // Root node of the Huffman tree
            public Node_mp3 Root { get; set; }

            // Frequencies of symbols
            public Dictionary<byte, int> Frequencies = new Dictionary<byte, int>();


            // Build the Huffman tree using the given source data
            public void Build(byte[] source)
            {

                // Calculate the frequencies of symbols in the source data
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                // Create nodes for each symbol-frequency pair and add them to the list of nodes
                foreach (KeyValuePair<byte, int> symbol in Frequencies)
                {
                    nodes.Add(new Node_mp3()
                    {
                        Symbol = symbol.Key,
                        Frequency = symbol.Value
                    });
                }

                // Build the Huffman tree by combining nodes until only one node (root) remains
                while (nodes.Count > 1)
                {
                    List<Node_mp3> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node_mp3>();

                    if (orderedNodes.Count >= 2)
                    {
                        List<Node_mp3> taken = orderedNodes.Take(2).ToList<Node_mp3>();
                        Node_mp3 parent = new Node_mp3()
                        {
                            Symbol = byte.MaxValue,
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };
                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    // Set the root of the Huffman tree as the remaining node in the list
                    this.Root = nodes.FirstOrDefault();
                }
            }

            // Encode the source data using the built Huffman tree
            public BitArray Encode(byte[] source)
            {
                
                List<bool> encodedSource = new List<bool>();

                // Traverse the Huffman tree for each symbol in the source data and append the encoded data
                for (int i = 0; i < source.Length; i++)
                {
                    
                    List<bool> encodedSymbol = this.Root.Traverse_mp3(source[i], new List<bool>());
                    
                    encodedSource.AddRange(encodedSymbol);
                }


                // Convert the list of bools to a BitArray and return it
                BitArray bits = new BitArray(encodedSource.ToArray());
                return bits;
            }

            // Decode the given BitArray using the built Huffman tree and return the decoded data
            public byte[] Decode(BitArray bits)
            {
                
                Node_mp3 current = this.Root;
                List<byte> decoded = new List<byte>();

                // Traverse the Huffman tree based on each bit in the BitArray and decode the symbols
                foreach (bool bit in bits)
                {
                  
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    // If the current node is a leaf node, add the symbol to the decoded data and reset the current node to the root
                    if (IsLeaf(current))
                    {
                        decoded.Add(current.Symbol);
                        current = this.Root;
                    }
                }

                // Convert the list of bytes to an array and return it as the decoded data
                return decoded.ToArray();
            }

            // Check if the given node is a leaf node
            public bool IsLeaf(Node_mp3 node)
            {
               
                return (node.Left == null && node.Right == null);
            }
        }

        // Utility method to write a BitArray to a BinaryWriter
        public static void WriteBitArray(BinaryWriter writer, BitArray bits)
        {
            // Convert the BitArray to a byte array and write it to the BinaryWriter
            byte[] bytes = new byte[(bits.Length + 7) / 8];
            bits.CopyTo(bytes, 0);
            writer.Write(bytes);
        }

        // Utility method to read a BitArray from a BinaryReader with the specified byte count
        public static BitArray ReadBitArray(BinaryReader reader, long byteCount)
        {
           
            List<bool> bits = new List<bool>();

            // Read the bytes from the BinaryReader and convert each byte to bits
            byte[] bytes = reader.ReadBytes((int)byteCount);

            foreach (byte b in bytes)
            {
                for (int i = 0; i < 8; i++)
                {
                    bits.Add((b & (1 << i)) != 0);
                }
            }

            // Convert the list of bools to a BitArray and return it
            return new BitArray(bits.ToArray());
        }
    }
}