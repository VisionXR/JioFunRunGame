using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public Vector3 Offset;
    public GameObject Player1, Player2;
    GameObject FollowPlayer;
    void Start()
    {
        if(JioNetworkmanager.instance.isMaster())
        {
            FollowPlayer = Player1;
            
        }
        else
        {
            FollowPlayer = Player2;
        }
    }

    void LateUpdate()
    {
        transform.position = FollowPlayer.transform.position + Offset;
    }
}
