using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BFS
{
    class Node
    {
        private int _value = 0;

        //all neighbors of this Node
        private List<string> _neighbors = new List<string>();
        private bool _visited = false;
        private string _name = "";

        public int Value { get; set; }
        public List<string> Neighbors { get; set; } = new List<string>();
        public bool Visited { get; set; } = false;
        public string Name { get => _name; set => _name = value; }

        public string ListNeighborsToString()
        {
            string ans = "";
            foreach (var neighbor in Neighbors)
            {
                ans += neighbor.ToString();
                ans += ", ";
            }

            ans = ans.Substring(0, ans.Length - 2);

            return ans;
        }
    }
}
