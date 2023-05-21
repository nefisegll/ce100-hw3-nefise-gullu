using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ce100_hw3_algo_lib_cs
{
    /// <summary>
    /// Represents an assembly guide that determines the order in which items should be assembled based on their dependencies.
    /// </summary>
    public class AssemblyGuide
    {
        // Stores the dependencies for each item
        private Dictionary<string, List<string>> dependencies;

        // Stores the order in which items should be assembled
        private List<string> assemblyOrder;

        // Keeps track of visited items during the topological sort
        private HashSet<string> visited;

        // Keeps track of items in the recursion stack during DFS
        private HashSet<string> recursionStack;

        // Stores the description of each item
        private Dictionary<string, string> itemDescriptions;

        // Initializes a new instance of the AssemblyGuide class.
        public AssemblyGuide()
        {
            dependencies = new Dictionary<string, List<string>>();
            assemblyOrder = new List<string>();
            visited = new HashSet<string>();
            recursionStack = new HashSet<string>();
            itemDescriptions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds a dependency for an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="dependencies">The dependencies of the item.</param>
        public void AddItemDependency(string item, List<string> dependencies)
        {
            if (!this.dependencies.ContainsKey(item))
                this.dependencies[item] = new List<string>();

            this.dependencies[item].AddRange(dependencies);
        }

        /// <summary>
        /// Adds a description for an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="description">The description of the item.</param>
        public void AddItemDescription(string item, string description)
        {
            itemDescriptions[item] = description;
        }

        /// <summary>
        /// Gets the assembly steps in the correct order.
        /// </summary>
        /// <returns>An ArrayList containing the assembly steps.</returns>
        public ArrayList GetAssemblySteps()
        {
            ArrayList steps = new ArrayList();
            foreach (string item in assemblyOrder)
            {
                string description = itemDescriptions[item];
                steps.Add(description);
            }
            return steps;
        }

        //Performs the topological sort to determine the assembly order.
        public void PerformTopologicalSort()
        {
            // Iterate through each item in the dependencies
            foreach (string item in dependencies.Keys)
            {
                if (!visited.Contains(item))
                {
                    if (DFS(item))
                    {
                        // If a circular dependency is detected, throw an exception
                        throw new Exception("Circular dependency detected!");
                    }
                }
            }

            // Reverse the assemblyOrder list to get the correct order
            assemblyOrder.Reverse();
        }

        /// <summary>
        /// Performs depth-first search (DFS) to check for cycles and determine the order of items.
        /// </summary>
        /// <param name="item">The current item being visited.</param>
        /// <returns>True if a cycle is detected, false otherwise.</returns>
        private bool DFS(string item)
        {
            visited.Add(item);
            recursionStack.Add(item);

            if (dependencies.ContainsKey(item))
            {
                // Iterate through each dependency of the current item
                foreach (string dependency in dependencies[item])
                {
                    if (!visited.Contains(dependency))
                    {
                        // Recursively call DFS on the dependency
                        if (DFS(dependency))
                            return true;
                    }
                    else if (recursionStack.Contains(dependency))
                    {
                        // If the dependency is already in the recursion stack, it means a cycle exists
                        return true;
                    }
                }
            }

            recursionStack.Remove(item);
            assemblyOrder.Add(item);
            return false;
        }
    }    
}
