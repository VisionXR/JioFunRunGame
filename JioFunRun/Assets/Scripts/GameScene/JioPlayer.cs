
using UnityEngine;
using Photon.Pun;
using System;

public class JioPlayer : MonoBehaviourPunCallbacks
{
    public float MoveSpeed;
    void Start()
    {
        JioInputManager.Instance.Touched += OnPlayerTouched;
        JioNetworkmanager.Instance.ReceiveOtherPlayerPosition += OnPlayerPositionRecieved;
    }

    private void OnPlayerPositionRecieved(Vector3 obj)
    {
        transform.position = obj;
    }

    private void OnPlayerTouched()
    {
        Debug.Log("It came in to On PlayerTouched");
        if(JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            Debug.Log("It came inside if condition Player 1 to On PlayerTouched");
        }
        else if(!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            Debug.Log("It came inside if condition Player 2 to On PlayerTouched");

        }
        JioNetworkmanager.Instance.SendPlayerData(transform.position);
       
    }

    
}
