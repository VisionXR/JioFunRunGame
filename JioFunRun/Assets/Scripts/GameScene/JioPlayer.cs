
using UnityEngine;
using TMPro;
using System;

public class JioPlayer : MonoBehaviour
{
    public static JioPlayer Instance;
    public float MoveSpeed;
    public bool isCollided;
   
    void Start()
    {
        Instance = this;
        JioInputManager.Instance.Touched += OnPlayerTouched;
        JioNetworkmanager.Instance.ReceiveOtherPlayerPosition += OnPlayerPositionRecieved;
        if(JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnPlayerPositionRecieved(Vector3 obj)
    {
        if (JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            transform.position = obj;
        }
        else if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {

            transform.position = obj;
        }
       
    }

    private void OnPlayerTouched()
    {
       
        if(JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            JioNetworkmanager.Instance.SendPlayerData(transform.position);
        }
        else if(!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            JioNetworkmanager.Instance.SendPlayerData(transform.position);
        }
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LevelGameObject")
        {
            isCollided = true;
            Debug.Log("The collided object was " + collision.gameObject.name);
            JioNetworkmanager.Instance.OnPlayerCollidedWithObject();
          
        }
    }


}
