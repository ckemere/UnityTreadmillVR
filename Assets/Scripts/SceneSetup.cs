using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject maze;
    public GameObject ForwardTeleport;
    public GameObject ForwardTeleportTarget;
    public GameObject RearTeleport;
    public GameObject RearTeleportTarget;

    public int numberOfCopies;

    public float hysteresis;

    // Start is called before the first frame update
    void Start()
    {
        // Figure how big maze is. Go through this process in case it gets
        // more complicated eventually.
        Bounds bounds = new Bounds(maze.transform.position, Vector3.zero);
 
        foreach(Renderer renderer in maze.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        // Clone maze ahead to give perception of an infinite environment
        Vector3 currentOffset = new Vector3();
        Quaternion rotation0 = new Quaternion();
        for (int i = 0; i < numberOfCopies; i++)
        {
            currentOffset.x += bounds.size.x;
            Object.Instantiate(maze, currentOffset, rotation0);
        }

        // Clone maze behind so that when we land off the end, that's set up
        currentOffset.x = -bounds.size.x;
        Object.Instantiate(maze, currentOffset, rotation0);


        // Now, set up teleporters in the right positions:
        // (1) One at the forward end of the maze
        ForwardTeleport.transform.position = new Vector3(bounds.max.x,0.0f,0.0f);
        ForwardTeleportTarget.transform.position = new Vector3(bounds.min.x, 0.0f, 0.0f);

        // (1) One at the backward end of the maze
        RearTeleport.transform.position = new Vector3(bounds.min.x - hysteresis,0.0f,0.0f);
        RearTeleportTarget.transform.position = new Vector3(bounds.max.x  - hysteresis,0.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
