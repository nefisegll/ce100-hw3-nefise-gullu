using ce100_hw3_algo_lib_cs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_test_cs
{
    public class AssemblyGuideTests
    {
        [Fact]
        public void TestAssemblyGuide()
        {
            AssemblyGuide assemblyGuide = new AssemblyGuide();

            
            assemblyGuide.AddItemDependency("Item1", new List<string> { "Item2", "Item3" });
            assemblyGuide.AddItemDependency("Item2", new List<string> { "Item4" });
            assemblyGuide.AddItemDependency("Item3", new List<string> { "Item4" });
            assemblyGuide.AddItemDependency("Item4", new List<string> { "Item5" });

          
            assemblyGuide.AddItemDescription("Item1", "Assemble base");
            assemblyGuide.AddItemDescription("Item2", "Attach legs");
            assemblyGuide.AddItemDescription("Item3", "Attach support beams");
            assemblyGuide.AddItemDescription("Item4", "Attach tabletop");
            assemblyGuide.AddItemDescription("Item5", "Attach additional accessories");

          
            assemblyGuide.PerformTopologicalSort();

         
            ArrayList steps = assemblyGuide.GetAssemblySteps();

            
            Assert.Equal("Assemble base", steps[0]);
            Assert.Equal("Attach support beams", steps[1]);
            Assert.Equal("Attach legs", steps[2]);
            Assert.Equal("Attach tabletop", steps[3]);
            Assert.Equal("Attach additional accessories", steps[4]);
        }
    }
}
