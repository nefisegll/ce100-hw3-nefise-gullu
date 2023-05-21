using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_lib_cs
{
    /// <summary>
    /// Represents a city road network and provides methods to find the shortest path between two vertices.
    /// </summary>
    public class CityRoadNetwork
    {
        // Number of vertices in the road network
        private int vertices;

        // List of edges in the road network
        private List<Edge> edges;

        /// <summary>
        /// Initializes a new instance of the CityRoadNetwork class with the specified number of vertices.
        /// </summary>
        /// <param name="v">Number of vertices in the road network.</param>
        public CityRoadNetwork(int v)
        {
            vertices = v;
            edges = new List<Edge>();
        }

        /// <summary>
        /// Adds a new edge to the road network.
        /// </summary>
        /// <param name="source">Source vertex of the edge.</param>
        /// <param name="destination">Destination vertex of the edge.</param>
        /// <param name="weight">Weight of the edge.</param>
        public void AddEdge(int source, int destination, int weight)
        {
            edges.Add(new Edge(source, destination, weight));
        }

        /// <summary>
        /// Finds the shortest path from a source vertex to a destination vertex in the road network.
        /// </summary>
        /// <param name="source">Source vertex of the path.</param>
        /// <param name="destination">Destination vertex of the path.</param>
        /// <returns>List of vertices representing the shortest path.</returns>
        public List<int> FindShortestPath(int source, int destination)
        {
            // Array to store the minimum distances from the source vertex to other vertices
            int[] distances = new int[vertices];

            // Array to store the previous vertex in the shortest path
            int[] prev = new int[vertices];

            // Initialize distances and prev arrays
            for (int i = 0; i < vertices; i++)
            {
                // Set initial distance to infinity
                distances[i] = int.MaxValue;

                // Set initial previous vertex to -1
                prev[i] = -1;
            }

            // Set distance of source vertex to 0
            distances[source] = 0;

            // Relax edges to find the shortest path
            for (int i = 0; i < vertices - 1; i++)
            {
                foreach (Edge edge in edges)
                {
                    // If the distance to the destination vertex can be minimized through the current edge, update the distance and previous vertex
                    if (distances[edge.Source] != int.MaxValue &&
                        distances[edge.Source] + edge.Weight < distances[edge.Destination])
                    {
                        distances[edge.Destination] = distances[edge.Source] + edge.Weight;
                        prev[edge.Destination] = edge.Source;
                    }
                }
            }

            // Check for negative-weight cycles
            foreach (Edge edge in edges)
            {
                if (distances[edge.Source] != int.MaxValue &&
                    distances[edge.Source] + edge.Weight < distances[edge.Destination])
                {
                    throw new Exception("Graph contains a negative-weight cycle.");
                }
            }

            // Reconstruct the shortest path
            List<int> path = new List<int>();
            int current = destination;
            while (current != -1)
            {
                path.Insert(0, current);
                current = prev[current];
            }

            return path;
        }
    }

    // Represents an edge between two vertices in the road network
    public class Edge
    {
        // Source vertex of the edge
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Edge(int source, int destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }
}
