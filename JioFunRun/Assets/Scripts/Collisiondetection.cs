using System;
using UnityEngine;

public class Collisiondetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {          
           JioPlayer.Instance.PlayerCollided();            
        }
    }
}
