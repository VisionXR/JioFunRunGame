using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JioPlayer : MonoBehaviourPunCallbacks
{
    public GameObject Player1, Player2;
    void Start()
    {
        if (photonView.IsMine)
        {
            Player1.SetActive(true);
            Player2.SetActive(false);
        }
        else 
        {
            Player1.SetActive(false);
            Player2.SetActive(true);
        }
    }

    
    void Update()
    {
        
    }
}
