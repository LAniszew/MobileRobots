using System;
using System.Collections.Generic;
using System.Windows;


namespace Driver.AutomaticControl.TrajectoryGenerator {


public class PathFinding {

    public static List<Point> findPath(Grid grid, Point startPos, Point targetPos, bool allowDiagonals) {
       
        List<Node> pathInNodes = findPathNodes(grid, startPos, targetPos, allowDiagonals);

      
        List<Point> pathInPoints = new List<Point>();

        if (pathInNodes != null)
            foreach (var node in pathInNodes)
            {
                pathInPoints.Add(new Point(node.x, node.y));
            }

        return pathInPoints;
    }

   
    private static List<Node> findPathNodes(Grid grid, Point startPos, Point targetPos, bool allowDiagonals) {
        Node startNode = grid.nodes[(int) startPos.X,(int) startPos.Y];
        Node targetNode = grid.nodes[(int) targetPos.X,(int) targetPos.Y];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node currentNode = openSet[0];

            for (int k = 1; k < openSet.Count; k++) {
                Node open = openSet[k];

                if (open.getFCost() < currentNode.getFCost() ||
                        open.getFCost() == currentNode.getFCost() &&
                                open.hCost < currentNode.hCost)
                    currentNode = open;
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
                return retracePath(startNode, targetNode);

            List<Node> neighbours;
            if (allowDiagonals) neighbours = grid.get8Neighbours(currentNode);
            else neighbours = grid.get4Neighbours(currentNode);

            foreach (Node neighbour in neighbours) {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour) * (int) (10.0f * neighbour.price);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    private static List<Node> retracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse(0, path.Count);
        return path;
    }

    private static int getDistance(Node nodeA, Node nodeB) {
        int distanceX = Math.Abs(nodeA.x - nodeB.x);
        int distanceY = Math.Abs(nodeA.y - nodeB.y);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
}