using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWrapTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;

    void OnTriggerEnter(Collider Other)
    {
        Debug.Log("Player entered trigger."); 
        player.transform.position = teleportTarget.transform.position;
    }

}
