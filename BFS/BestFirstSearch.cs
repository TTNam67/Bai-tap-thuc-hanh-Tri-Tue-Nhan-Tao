using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    internal class BestFirstSearch
    {
        //Contains information of every node in the graph
        public List<Node> _nodeList = new List<Node>();
        int _id = 0;

        int n; //number of vertices
        int k; //number of edges
        string _startP, _endP; //startPoint and endPoint

        //Store the: {vertex name - its index in _nodeList}
        Dictionary<string, int> mapVertices = new Dictionary<string, int>();

        List<string> path = new List<string>();
        public string stringToExport;

        public BestFirstSearch(string[] data)
        {
            n = int.Parse(data[0]); //number of vertices

            PreDefineMap(data[1]); // Define for the map that contains: <vertices name> : <its id>
            k = int.Parse(data[2]); //number of edges

            for (int i = 3; i < (3 + n); i++)
            {
                Node node = new Node();

                //eg: A B C E
                //data[i] = "A B C E\r"
                data[i] = data[i].Substring(0, data[i].Length - 1);
                data[i] = data[i].TrimEnd(' ');

                string[] strings = data[i].Split(new string[] { " " }, StringSplitOptions.None);

                //The current vertex: A
                node.Name = strings[0];

                //Add all the neighbor vertices into a list: C D E
                List<string> neighbors = new List<string>();

                for (int j = 1; j < strings.Length; j++)
                {
                    neighbors.Add(strings[j]);

                }

                //Save "all the neighbor vertices" in the "current vertex"
                node.Neighbors = neighbors;

                _nodeList.Add(node);
            }

            //start point and end point
            string[] tmp = data[n + 3].Split(new string[] { " " }, StringSplitOptions.None);
            _startP = tmp[0];
            _endP = tmp[1];
            _endP = _endP.Substring(0, _endP.Length - 1);
            Console.WriteLine($"start: {_startP}, end: {_endP}");

            //Add weight to vertices
            for (int i = n + 4; i < 2*n + 4; i++)
            {
                tmp = data[i].Split(new string[] { " " }, StringSplitOptions.None);
                _nodeList[i - (n + 4)].Value = int.Parse(tmp[1]);
            }
        }

        public void PreDefineMap(string data)
        {
            data = data.Substring(0, data.Length - 1);
            string[] nameList = data.Split(new string[] { " " }, StringSplitOptions.None);
            foreach (string name in nameList)
            {
                Console.WriteLine($"{name}: {_id}"); ;
                mapVertices[name] = _id;
                _id++;
            }

            _id = 0; //reset id for another purpose 
        }

        public bool Solve()
        {
            int numOfDash = 33;
            string spacePerCell = string.Concat(Enumerable.Repeat(' ', numOfDash));
            string dashPerCell = string.Concat(Enumerable.Repeat('-', numOfDash));

            string TrangThaiPhatTrien = "Trạng thái phát triển";
            string TrangThaiKe = "Trạng thái kề";
            string DanhSachL = "Danh sách L";

            stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";

            stringToExport += "+";
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiPhatTrien.Length) / 2));
            stringToExport += TrangThaiPhatTrien;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiPhatTrien.Length) / 2));
            stringToExport += "+";


            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiKe.Length) / 2));
            stringToExport += TrangThaiKe;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiKe.Length) / 2));
            stringToExport += "+";


            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DanhSachL.Length) / 2));
            stringToExport += DanhSachL;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DanhSachL.Length) / 2));
            stringToExport += "+\n";


            stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";

            bool[] visited = new bool[n];
            List <string> q = new List <string>();
            q.Add(_startP);
            visited[mapVertices[_startP]] = true;

            while (q.Count > 0)
            {
                string curr = q[0];
                q.RemoveAt(0);

                // Thêm thông tin về trạng thái hiện tại 
                stringToExport += "| ";
                stringToExport += curr;
                stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - 2));


                if (curr == _endP)
                {
                    stringToExport += "| TTKT - Dừng";
                    stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - 12));
                    stringToExport += $"|{spacePerCell}|\n";
                    stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";
                    return true;
                }

                string adjacentList = "| ";

                foreach (string vertex in _nodeList[mapVertices[curr]].Neighbors)
                {
                    if (visited[mapVertices[vertex]] == false)
                    {
                        //Success
                        q.Add(vertex);
                        Console.WriteLine($"{curr}: {vertex}");
                        visited[mapVertices[vertex]] = true;
                    }
                }

                adjacentList += _nodeList[mapVertices[curr]].ListNeighborsToString();

                stringToExport += adjacentList;
                stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - adjacentList.Length + 1));
                stringToExport += "|";



                //Danh sách L 
                for (int i = 1; i < q.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (_nodeList[mapVertices[q[i]]].Value < _nodeList[mapVertices[q[j]]].Value) 
                        {
                            string tmp = q[i];
                            q[i] = q[j];
                            q[i] = tmp;
                        }
                    }
                }

                string listL = " ";
                foreach(string a in q)
                {
                    listL += $"{a}, ";
                }


                if (listL.Length > 2)
                    listL = listL.Substring(0, listL.Length - 2);
                listL += string.Concat(Enumerable.Repeat(' ', numOfDash - listL.Length));
                stringToExport += listL;


                stringToExport += "|\n";


            }

            return false;
        }
    }
}
