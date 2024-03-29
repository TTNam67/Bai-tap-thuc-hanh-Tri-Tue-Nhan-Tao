using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alpha_Beta
{
	internal class Alpha_Beta
	{
		public List<Node> _nodeList = new List<Node>();
		int _id = 0;
		int n; //number of vertices
		int k; //number of edges

		//Store the: {vertex name - its index in _nodeList}
		Dictionary<string, int> mapVertices = new Dictionary<string, int>();
		Dictionary<string, int> _mapRankVertices = new Dictionary<string, int>();
		int _rankID = 0;

		//Stores the visited node 
		List<string> _visited = new List<string>();
		public string stringToExport;
		List<string> path = new List<string>();

		public Alpha_Beta(string[] data) {
			n = int.Parse(data[0]);

		}
	}
}
