using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace WarehouseSimulator.Utils
{
    public static class Pathfinder
    {
        public static List<(int x, int y)> FindPath((int x, int y) start, (int x, int y) target, int[,] grid)
        {
            var openSet = new List<Node>();
            var closedSet = new HashSet<(int x, int y)>();
            var cameFrom = new Dictionary<(int x, int y), (int x, int y)>();

            // Check if we're already adjacent to target
            if (IsAdjacent(start, target))
                return new List<(int x, int y)> { start };

            openSet.Add(new Node(start.x, start.y, 0, Heuristic(start, target)));

            while (openSet.Count > 0)
            {
                openSet.Sort((a, b) => a.FCost.CompareTo(b.FCost));
                var current = openSet[0];
                openSet.RemoveAt(0);

                // Modified target check - stop if adjacent to target
                if (IsAdjacent((current.x, current.y), target))
                {
                    var path = ReconstructPath(cameFrom, current);
                    path.Add((current.x, current.y)); // Add current position
                    return path;
                }

                closedSet.Add((current.x, current.y));

                foreach (var neighbor in GetNeighbors(current, grid))
                {
                    if (closedSet.Contains(neighbor)) continue; 

                    // Allow movement through empty spaces OR the target position
                    if (grid[neighbor.x, neighbor.y] == 1 && !(neighbor.x == target.x && neighbor.y == target.y))
                        continue;

                    var tentativeG = current.gCost + 1;
                    var neighborNode = openSet.FirstOrDefault(n => n.x == neighbor.x && n.y == neighbor.y);

                    if (neighborNode == null || tentativeG < neighborNode.gCost)
                    {
                        cameFrom[neighbor] = (current.x, current.y);
                        var hCost = Heuristic(neighbor, target);

                        if (neighborNode == null)
                        {
                            neighborNode = new Node(neighbor.x, neighbor.y, tentativeG, hCost);
                            openSet.Add(neighborNode);
                        }
                        else
                        {
                            neighborNode.gCost = tentativeG;
                            neighborNode.hCost = hCost;
                        }
                    }
                }
            }
            return null; // No path found
        }

        // Helper method to check adjacency
        private static bool IsAdjacent((int x, int y) pos, (int x, int y) target)
        {
            return (Math.Abs(pos.x - target.x) + Math.Abs(pos.y - target.y)) == 1;
        }

        private static int Heuristic((int x, int y) a, (int x, int y) b)
        => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y); // Manhattan distance

        private static List<(int x, int y)> ReconstructPath(Dictionary<(int x, int y), (int x, int y)> cameFrom ,Node current)
        {
            // 1. Create an empty path list
            var path = new List<(int x, int y)>();
            // 2. Backtrack from target to start
            while (cameFrom.ContainsKey((current.x, current.y)))
            {
                // 2a. Add current position to path
                path.Add((current.x, current.y));

                // 2b. Move to parent node (where we came from)
                current = new Node(cameFrom[(current.x, current.y)].x, cameFrom[(current.x, current.y)].y, 0, 0);
            }
            // 3. Reverse to get start→target order
            path.Reverse();
            return path;
        }

        private static IEnumerable<(int x, int y)> GetNeighbors(Node node, int[,] grid)
        {
            var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            foreach (var dir in directions)
            {
                int nx = node.x + dir.dx, ny = node.y + dir.dy;
                if (nx >= 0 && nx < grid.GetLength(0) && ny >= 0 && ny < grid.GetLength(1))
                    yield return (nx, ny);
            }
        }

    }
}
