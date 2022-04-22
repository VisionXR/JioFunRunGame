using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMRotation2 : MonoBehaviour
{
    public float Speed;
    public float X, Y, Z;
    public float LL, UL;
    public bool isClock = true;
    public float angle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClock)
        {

            transform.eulerAngles += new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles -= new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        if (X != 0 && transform.eulerAngles.x > UL)
        {
            isClock = false;
            angle = transform.eulerAngles.x;

        }
        if (X != 0 && transform.eulerAngles.x < LL)
        {
            isClock = true;
            angle = transform.eulerAngles.x;


        }
        if (Y != 0 && transform.eulerAngles.y > UL)
        {
            isClock = false;
            angle = transform.eulerAngles.y;


        }
        if (Y != 0 && transform.eulerAngles.y < LL)
        {
            isClock = true;
            angle = transform.eulerAngles.y;

        }
        if (Z != 0 && transform.eulerAngles.z > UL)
        {
            isClock = false;
            angle = transform.eulerAngles.z;

        }
        if (Z != 0 && transform.eulerAngles.z < LL)
        {
            isClock = true;
            angle = transform.eulerAngles.z;

        }
    }

}
