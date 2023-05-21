using ce100_hw3_algo_lib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_test_cs
{
    public class TreePipelineSystemTests
    {
        [Fact]
        public void ComputeMST_ReturnsValidMSTEdges()
        {
            
            int numTrees = 10;
            TreePipelineSystem treePipelineSystem = new TreePipelineSystem(numTrees);

            
            treePipelineSystem.ComputeMST();
            var mstEdges = treePipelineSystem.GetMSTEdges();

            
            Assert.NotNull(mstEdges);
            Assert.Equal(numTrees - 1, mstEdges.Count); // MST should have numTrees - 1 edges
        }

        [Fact]
        public void ComputeMST_ThrowsArgumentException_WhenNumTreesLessThan10()
        {
           
            int numTrees = 5;

            
            Assert.Throws<ArgumentException>(() => new TreePipelineSystem(numTrees));
        }
    }
}
