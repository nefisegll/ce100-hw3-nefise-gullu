using ce100_hw3_algo_lib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_test_cs
{
    public class CityRoadNetworkTests
    {
        [Fact]
        public void FindShortestPath_ShouldReturnShortestPath()
        {
            CityRoadNetwork network = new CityRoadNetwork(6);
            network.AddEdge(0, 1, 2);
            network.AddEdge(0, 2, 5);
            network.AddEdge(1, 2, -2);
            network.AddEdge(1, 3, 1);
            network.AddEdge(2, 3, 3);
            network.AddEdge(2, 4, 1);
            network.AddEdge(3, 4, 4);
            network.AddEdge(3, 5, 3);
            network.AddEdge(4, 5, 1);

            List<int> path = network.FindShortestPath(0, 5);

            
            Assert.Equal(new List<int> { 0, 1, 2, 4, 5 }, path);
        }
    }
}
