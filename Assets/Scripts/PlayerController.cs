using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //private Rigidbody rb;

    private UdpReceiver receiver;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        int receivePort = 20001;
    
        receiver = new UdpReceiver();
        receiver.StartReceiver(receivePort);
    }

    // Fixed Update is called once per physics tick
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0, 0); // x, y, z
        
        transform.position += movement*speed;
        //rb.AddForce (movement*speed);

        // SendUDPString(transform.position.x.ToString());

        foreach (var message in receiver.getMessages()) Debug.Log(message);

    }
    void OnDestroy()
    {
        receiver.Stop();
    }
     

}
