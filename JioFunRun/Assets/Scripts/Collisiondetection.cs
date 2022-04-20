using System;
using UnityEngine;

public class Collisiondetection : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action Collided;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Collided != null)
            {
                JioPlayer.Instance.PlayerCollided();
            }
        }
    }


}
