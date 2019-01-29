using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWrapTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    public bool IsFront;

    private Vector3 playerInitialPos;
    private float playerOffset;
    void Start ()
    {
        playerInitialPos = player.transform.position; // Players never move in y or z
        playerOffset = player.GetComponent<Renderer>().bounds.extents.x;
    }

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Player entered trigger." + teleportTarget.name); 
        if (IsFront)
            player.transform.position =  
                new Vector3(teleportTarget.transform.position.x - playerOffset,
                    playerInitialPos.y, playerInitialPos.z);
        else
            player.transform.position =  
                new Vector3(teleportTarget.transform.position.x + playerOffset,
                    playerInitialPos.y, playerInitialPos.z);
    }

}
