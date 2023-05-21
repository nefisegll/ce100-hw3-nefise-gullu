using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_lib_cs
{
    /// <summary>
    /// Represents a tree pipeline system that connects multiple trees.
    /// </summary>

    public class TreePipelineSystem
    {
        // Number of trees in the system
        private int numTrees;

        // Distance matrix between trees
        private double[,] distances;

        // List of edges in the minimum spanning tree (MST)
        private List<Edge> mstEdges;

        /// <summary>
        /// Initializes a new instance of the TreePipelineSystem class with the specified number of trees.
        /// </summary>
        /// <param name="numTrees">Number of trees in the system.</param>

        public TreePipelineSystem(int numTrees)
        {
            if (numTrees < 10)
                throw new ArgumentException("Number of trees must be at least 10");

            this.numTrees = numTrees;
            this.distances = new double[numTrees, numTrees];
            this.mstEdges = new List<Edge>();

            GenerateRandomTreeLocations();
            CalculateDistances();
        }

        /// <summary>
        /// Computes the minimum spanning tree (MST) of the tree pipeline system.
        /// </summary>
        public void ComputeMST()
        {
            List<Edge> edges = BuildAllEdges();

            edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

            DisjointSet<int> disjointSet = new DisjointSet<int>(numTrees);

            mstEdges.Clear();

            foreach (Edge edge in edges)
            {
                if (!disjointSet.AreInSameSet(edge.StartNode, edge.EndNode))
                {
                    disjointSet.MergeSets(edge.StartNode, edge.EndNode);
                    mstEdges.Add(edge);
                }
            }
        }

        /// <summary>
        /// Gets the edges of the minimum spanning tree (MST).
        /// </summary>
        /// <returns>An ArrayList of strings representing the edges of the MST.</returns>
        public ArrayList GetMSTEdges()
        {
            ArrayList edgeList = new ArrayList();
            foreach (Edge edge in mstEdges)
            {
                edgeList.Add($"Edge from tree {edge.StartNode} to tree {edge.EndNode} with length {edge.Weight:F2}");
            }
            return edgeList;
        }

        // Generates random coordinates for tree locations
        private void GenerateRandomTreeLocations()
        {
            Random rnd = new Random();
            for (int i = 0; i < numTrees; i++)
            {
                double x = rnd.NextDouble() * 100;
                double y = rnd.NextDouble() * 100;

                // Distance from a tree to itself is 0
                distances[i, i] = 0; 

                // X-coordinate of tree i
                distances[i, i] = x;

                // Y-coordinate of tree i
                distances[i, i] = y;
            }
        }

        // Calculates the distances between trees based on their coordinates
        private void CalculateDistances()
        {
            for (int i = 0; i < numTrees; i++)
            {
                for (int j = i + 1; j < numTrees; j++)
                {
                    // Difference in X-coordinates
                    double dx = distances[j, 0] - distances[i, 0];

                    // Difference in Y-coordinates
                    double dy = distances[j, 1] - distances[i, 1];

                    // Euclidean distance
                    distances[i, j] = distances[j, i] = Math.Sqrt(dx * dx + dy * dy);
                }
            }
        }

        // Builds all possible edges between trees
        private List<Edge> BuildAllEdges()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < numTrees - 1; i++)
            {
                for (int j = i + 1; j < numTrees; j++)
                {
                    double distance = distances[i, j];
                    edges.Add(new Edge(i, j, distance));
                }
            }
            return edges;
        }

        private class Edge
        {
            public int StartNode { get; }
            public int EndNode { get; }
            public double Weight { get; }

            public Edge(int startNode, int endNode, double weight)
            {
                StartNode = startNode;
                EndNode = endNode;
                Weight = weight;
            }
        }

        // Represents a disjoint set data structure for efficient set operations
        private class DisjointSet<T>
        {
            private int[] parent;
            private int[] rank;

            public DisjointSet(int size)
            {
                parent = new int[size];
                rank = new int[size];

                for (int i = 0; i < size; i++)
                {
                    parent[i] = i;
                    rank[i] = 0;
                }
            }

            public int FindSet(int element)
            {
                if (element != parent[element])
                {
                    parent[element] = FindSet(parent[element]);
                }

                return parent[element];
            }

            public bool AreInSameSet(int element1, int element2)
            {
                return FindSet(element1) == FindSet(element2);
            }

            public void MergeSets(int element1, int element2)
            {
                int root1 = FindSet(element1);
                int root2 = FindSet(element2);

                if (root1 == root2)
                    return;

                if (rank[root1] < rank[root2])
                {
                    parent[root1] = root2;
                }
                else if (rank[root1] > rank[root2])
                {
                    parent[root2] = root1;
                }
                else
                {
                    parent[root1] = root2;
                    rank[root2]++;
                }
            }
        }
    }

}
