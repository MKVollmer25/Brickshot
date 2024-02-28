using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        //Debug.Log("Edge Normal: " + CollisionInfo.GetContact(0).normal);
    }
}
