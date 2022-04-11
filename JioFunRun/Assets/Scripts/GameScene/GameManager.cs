
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    void Start()
    {
        Debug.Log(" It came in to start of game manager");
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log(" It came in to if cond");
            if (PlayerPrefab != null)
            {
                PhotonNetwork.Instantiate(PlayerPrefab.name, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Debug.Log("PlayerPreFab Is Null");
            }
        }
    }

    
    void Update()
    {
        
    }
}
