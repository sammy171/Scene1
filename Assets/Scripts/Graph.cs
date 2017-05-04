using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public float nodeRadius = 1f;
    public LayerMask unwalkableMask;
    public Node[,] nodes;

    private float nodeDiameter;
    private int gridSizeX, gridSizeZ;
    private Vector3 scale;
    private Vector3 halfScale;
    private Vector3 bottomLeft;

    void Start()
    {
        CreateGrid();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        if(nodes != null)
        {
            // Loop through all nodes in grid
            for (int X = 0; X < nodes.GetLength(0); X++)
            {
                for (int z = 0; z < nodes.GetLength(1); z++)
                {
                    Node node = nodes[X, z];

                    Gizmos.color = node.walkable ? new Color(0, 0, 1, 0.5f) : new Color(1, 0, 0, 0.5f);
                    Gizmos.DrawSphere(node.position, nodeRadius);
                }
            }
        }
    }

    public void CreateGrid()
    {
        // Calculate node diameter
        nodeDiameter = nodeRadius * 2;

        // Get the transform's scale and halfscale
        scale = transform.localScale;
        halfScale = scale / 2f;

        // Calculate grid size in int form
        gridSizeX = Mathf.RoundToInt(halfScale.x / nodeRadius);
        gridSizeZ = Mathf.RoundToInt(halfScale.z / nodeRadius);

        // Create the grid of that size (2D array)
        nodes = new Node[gridSizeX, gridSizeZ];

        // Calculate bottom left corner
        bottomLeft = transform.position - Vector3.right * halfScale.x - Vector3.forward * halfScale.z;

        // Loop through all nodes in the grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                // Calculate offsets
                float xOffset = x * nodeDiameter + nodeRadius;
                float zOffset = z * nodeDiameter + nodeRadius;

                //Calculate the world point of the node position
                Vector3 nodePos = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;

                // Use physics to check if it collided with a non-walkable object
                bool walkable = !Physics.CheckSphere(nodePos, nodeRadius, unwalkableMask);

                // Create a new node in that index
                nodes[x, z] = new Node(walkable, nodePos, x, z);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Loop through all the nodes
        for (int x = 0; x < nodes.GetLength(0); x++)
        {
            for (int z = 0; z < nodes.GetLength(1); z++)
            {
                // Calculate offsets
                float xOffset = x * nodeDiameter + nodeRadius;
                float zOffset = z * nodeDiameter + nodeRadius;

                //Calculate the world point of the node position
                Vector3 nodePos = bottomLeft + Vector3.right * xOffset + Vector3.forward * zOffset;


                Node node = nodes[x, z];
                // Use physics to check if it collided with a non-walkable object
                node.walkable = !Physics.CheckSphere(nodePos, nodeRadius, unwalkableMask);

            }
        }
    }
            
}
