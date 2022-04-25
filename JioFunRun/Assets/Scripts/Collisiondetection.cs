using UnityEngine;

public class Collisiondetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (JioPlayer.instance != null)
            {
               //  JioPlayer.instance.PlayerCollided();   
            }
            if(SinglePlayer.instance != null)
            {
                SinglePlayer.instance.PlayDeadAnim();
            }
        }
    }
}
