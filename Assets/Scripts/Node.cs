using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 position;
    public int gridX, gridZ;

    public int gCost; // distance from start node
    public int hCost; // distance to target
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    
    public Node(bool walkable, Vector3 position, int gridX, int gridZ)
    {
        this.walkable = walkable;
        this.position = position;
        this.gridX = gridX;
        this.gridZ = gridZ;
    }

    
}
