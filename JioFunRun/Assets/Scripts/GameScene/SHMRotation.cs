
using UnityEngine;

public class SHMRotation : MonoBehaviour
{
    public float Speed;
    public float X, Y, Z;
    public float LL, UL;
    public bool isClock = true;
    public float angle;
   
    void Update()
    {
        if (isClock)
        {
         
            transform.localEulerAngles += new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        else
        {
            transform.localEulerAngles -= new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        if (X != 0 && transform.localEulerAngles.x > UL)
        {
            isClock = false;
            angle = transform.localEulerAngles.x;

        }
        if (X != 0 && transform.localEulerAngles.x < LL)
        {
            isClock = true;
            angle = transform.localEulerAngles.x;


        }
        if (Y != 0 && transform.localEulerAngles.y > UL)
        {
            isClock = false;
            angle = transform.localEulerAngles.y;


        }
        if (Y != 0 && transform.localEulerAngles.y < LL)
        {
            isClock = true;
            angle = transform.localEulerAngles.y;

        }
        if (Z != 0 && transform.localEulerAngles.z > UL)
        {
            isClock = false;
            angle = transform.localEulerAngles.z;

        }
        if (Z != 0 && transform.localEulerAngles.z < LL)
        {
            isClock = true;
            angle = transform.localEulerAngles.z;

        }
    }
}

