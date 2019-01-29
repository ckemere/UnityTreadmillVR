using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Fixed Update is called once per physics tick
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0, 0); // x, y, z
        
        transform.position += movement*speed;
        //rb.AddForce (movement*speed);
    }
}
