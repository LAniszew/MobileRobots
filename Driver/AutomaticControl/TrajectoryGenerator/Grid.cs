using System.Collections.Generic;

namespace Driver.AutomaticControl.TrajectoryGenerator
{
   
public class Grid {
    public Node[,] nodes;
    int gridWidth, gridHeight;


    public Grid(int width, int height, float[,] tile_costs) {
        gridWidth = width;
        gridHeight = height;
        nodes = new Node[width,height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                nodes[x,y] = new Node(x, y, tile_costs[x,y]);
    }

  
    public Grid(int width, int height, bool[,] walkableTiles) {
        gridWidth = width;
        gridHeight = height;
        nodes = new Node[width,height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                nodes[x,y] = new Node(x, y, walkableTiles[x,y] ? 1.0f : 0.0f);
    }

    public List<Node> get8Neighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue;

                int checkX = node.x + x;
                int checkY = node.y + y;

                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                    neighbours.Add(nodes[checkX,checkY]);
            }

        return neighbours;
    }

    public List<Node> get4Neighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        if (node.y + 1 >= 0 && node.y + 1  < gridHeight) neighbours.Add(nodes[node.x,node.y + 1]); // N
        if (node.y - 1 >= 0 && node.y - 1  < gridHeight) neighbours.Add(nodes[node.x,node.y - 1]); // S
        if (node.x + 1 >= 0 && node.x + 1  < gridHeight) neighbours.Add(nodes[node.x + 1,node.y]); // E
        if (node.x - 1 >= 0 && node.x - 1  < gridHeight) neighbours.Add(nodes[node.x - 1,node.y]); // W

        return neighbours;
    }
}
}