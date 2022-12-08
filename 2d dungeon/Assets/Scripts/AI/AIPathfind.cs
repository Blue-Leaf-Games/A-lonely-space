using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace Main
{
    public class AIPathfind : MonoBehaviour
    {
        /*public List<Node> Nodes;
        public class MapLoc
        {
            public int x;
            public int y;
            public MapLoc(int o, int p)
            {
                x = o;
                y = p;
            }
        }
        public class Node
        {
            public MapLoc Loc;
            public GameObject Obj;
            public double StraightLineDistanceToEnd;
            public double MinCostToStart;
            public Node(MapLoc o, GameObject p)
            {
                Loc = o;
                Obj = p;
            }
            public double StraightLineDistanceTo(MapLoc End)
            {
                return Mathf.Sqrt((End.x - Loc.x) ^ 2 + (End.y - Loc.y) ^ 2);
            }
        }
        public void Start()
        {
            foreach (GameObject obj in publicvariables.MapObjects)
            {
                Nodes.Add(new Node(new MapLoc(int.Parse(((obj.transform.position.x - 0.08) / 0.16).ToString()), int.Parse(((obj.transform.position.y - 0.08) / 0.16).ToString())),obj));
            }
        }
        // found at https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp and then edited to fit current program
        public List<Node> GetShortestPathAstar(Node StartLoc, Node EndLoc)
        {
            foreach (Node node in Nodes)
            {
                node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(EndLoc.Loc);
            }
            AstarSearch(StartLoc);
            var shortestPath = new List<Node>();
            shortestPath.Add(EndLoc);
            BuildShortestPath(shortestPath, EndLoc);
            shortestPath.Reverse();
            return shortestPath;
        }
        private void BuildShortestPath(List<Node> list, Node node)
        {
            if (node.NearestToStart == null)
                return;
            list.Add(node.NearestToStart);
            BuildShortestPath(list, node.NearestToStart);
        }
        private void AstarSearch(Node StartLoc, Node EndLoc)
        {
            StartLoc.MinCostToStart = 0;
            List<Node> prioQueue = new List<Node>();
            prioQueue.Add(StartLoc);
            do
            {
                prioQueue = prioQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
                var node = prioQueue.First();
                prioQueue.Remove(node);
                NodeVisits++;
                foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
                {
                    var childNode = cnn.ConnectedNode;
                    if (childNode.Visited)
                        continue;
                    if (childNode.MinCostToStart == null ||
                        node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
                    {
                        childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
                        childNode.NearestToStart = node;
                        if (!prioQueue.Contains(childNode))
                            prioQueue.Add(childNode);
                    }
                }
                node.Visited = true;
                if (node == EndLoc)
                    return;
            } while (prioQueue.Any());
        }
        */

        // found at https://github.com/davecusatis/A-Star-Sharp/blob/master/Astar.cs
        /*
        public class Node
        {
            // Change this depending on what the desired size is for each element in the grid
            public static int NODE_SIZE = 32;
            public Node Parent;
            public Vector2 Position;
            public Vector2 Center
            {
                get
                {
                    return new Vector2(Position.x + NODE_SIZE / 2, Position.y + NODE_SIZE / 2);
                }
            }
            public float DistanceToTarget;
            public float Cost;
            public float Weight;
            public float F
            {
                get
                {
                    if (DistanceToTarget != -1 && Cost != -1)
                        return DistanceToTarget + Cost;
                    else
                        return -1;
                }
            }
            public bool Walkable;

            public Node(Vector2 pos, bool walkable, float weight = 1)
            {
                Parent = null;
                Position = pos;
                DistanceToTarget = -1;
                Cost = 1;
                Weight = weight;
                Walkable = walkable;
            }
        }

        public class Astar
        {
            List<List<Node>> Grid;
            int GridRows
            {
                get
                {
                    return Grid[0].Count;
                }
            }
            int GridCols
            {
                get
                {
                    return Grid.Count;
                }
            }

            public Astar(List<List<Node>> grid)
            {
                Grid = grid;
            }

            public Stack<Node> FindPath(Vector2 Start, Vector2 End)
            {
                Node start = new Node(new Vector2((int)(Start.x / Node.NODE_SIZE), (int)(Start.y / Node.NODE_SIZE)), true);
                Node end = new Node(new Vector2((int)(End.x / Node.NODE_SIZE), (int)(End.y / Node.NODE_SIZE)), true);

                Stack<Node> Path = new Stack<Node>();
                PriorityQueue<Node, float> OpenList = new PriorityQueue<Node, float>();
                
                List<Node> ClosedList = new List<Node>();
                List<Node> adjacencies;
                Node current = start;

                // add start node to Open List
                OpenList.Enqueue(start, start.F);

                while (OpenList.Count() != 0 && !ClosedList.Exists(x => x.Position == end.Position))
                {
                    current = OpenList.Dequeue();
                    ClosedList.Add(current);
                    adjacencies = GetAdjacentNodes(current);

                    foreach (Node n in adjacencies)
                    {
                        if (!ClosedList.Contains(n) && n.Walkable)
                        {
                            bool isFound = false;
                            foreach (var oLNode in OpenList.UnorderedItems)
                            {
                                if (oLNode.Element == n)
                                {
                                    isFound = true;
                                }
                            }
                            if (!isFound)
                            {
                                n.Parent = current;
                                n.DistanceToTarget = Math.Abs(n.Position.x - end.Position.x) + Math.Abs(n.Position.y - end.Position.y);
                                n.Cost = n.Weight + n.Parent.Cost;
                                OpenList.Enqueue(n, n.F);
                            }
                        }
                    }
                }

                // construct path, if end was not closed return null
                if (!ClosedList.Exists(x => x.Position == end.Position))
                {
                    return null;
                }

                // if all good, return path
                Node temp = ClosedList[ClosedList.IndexOf(current)];
                if (temp == null) return null;
                do
                {
                    Path.Push(temp);
                    temp = temp.Parent;
                } while (temp != start && temp != null);
                return Path;
            }

            private List<Node> GetAdjacentNodes(Node n)
            {
                List<Node> temp = new List<Node>();

                int row = (int)n.Position.y;
                int col = (int)n.Position.x;

                if (row + 1 < GridRows)
                {
                    temp.Add(Grid[col][row + 1]);
                }
                if (row - 1 >= 0)
                {
                    temp.Add(Grid[col][row - 1]);
                }
                if (col - 1 >= 0)
                {
                    temp.Add(Grid[col - 1][row]);
                }
                if (col + 1 < GridCols)
                {
                    temp.Add(Grid[col + 1][row]);
                }

                return temp;
            }
        }
        */
    }
}




