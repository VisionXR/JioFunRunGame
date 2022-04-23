// this is dummy script used for Player movement to see and  adjusting level gameobjects for view,
//remove this script at final and put jio player script  OK!!
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0, MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 0, MoveSpeed * Time.deltaTime);
        }
    }
}
