using System;
using UnityEngine;

public class JioPlayer : MonoBehaviour
{
    public static JioPlayer Instance;
    public float MoveSpeed;
    public bool isCollided;
    public Animator PlayerAnim;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {        
        JioInputManager.Instance.Touched += OnPlayerTouched;
        JioInputManager.Instance.StayIdle += OnPlayedIdle;
        JioNetworkmanager.Instance.ReceiveOtherPlayerPosition += OnPlayerPositionRecieved;
        JioNetworkmanager.Instance.ReceiveIdlePos += OnPlayerIdleReceived;
        if(JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnPlayerIdleReceived()
    {
        if (JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            PlayerAnim.SetBool("Walk", false);
        }
        else if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {

            PlayerAnim.SetBool("Walk", false);
        }
    }

    private void OnPlayedIdle()
    {
        if (JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {        
            PlayerAnim.SetBool("Walk", false);
            JioNetworkmanager.Instance.SendIdle();
        }
        else if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
          
            PlayerAnim.SetBool("Walk", false);
            JioNetworkmanager.Instance.SendIdle();
        }
    }

    private void OnPlayerPositionRecieved(Vector3 obj)
    {
        if (JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            transform.position = obj;
            PlayerAnim.SetBool("Walk", true);
        }
        else if (!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            transform.position = obj;
            PlayerAnim.SetBool("Walk", true);
        }
       
    }
    private void OnPlayerTouched()
    {
       
        if(JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player1")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            PlayerAnim.SetBool("Walk", true);
            JioNetworkmanager.Instance.SendPlayerData(transform.position);
        }
        else if(!JioNetworkmanager.Instance.isMaster() && gameObject.name == "Player2")
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            PlayerAnim.SetBool("Walk", true);
            JioNetworkmanager.Instance.SendPlayerData(transform.position);
        }       
       
    }
    private void OnCollisionEnter(Collision collision)
    {     
        if (collision.gameObject.tag == "LevelGameObject")
        {
            PlayerCollided();
        }
    }
    public void PlayerCollided()
    {
        isCollided = true;
        JioNetworkmanager.Instance.OnPlayerCollidedWithObject();
        RespawnThePlayer();

    }
    public void RespawnThePlayer()
    {
        Vector3 CheckPointPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        this.transform.position = CheckPointPosition;
    }


}
