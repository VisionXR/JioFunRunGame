
using UnityEngine;

public class SHMRotation : MonoBehaviour
{
    public float Speed;
    public float X, Y, Z;
    public float LL, UL;
    public bool isClock = true;
    void Start()
    {
        
    }

    
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
        }
        if (X != 0 && transform.localEulerAngles.x < LL)
        {
            isClock = true;

        }
        if (Y != 0 && transform.localEulerAngles.y > UL)
        {
            isClock = false;
           
        }
        if (Y != 0 && transform.localEulerAngles.y < LL)
        {
            isClock = true;
           
        }
        if (Z != 0 && transform.localEulerAngles.z > UL)
        {
            isClock = false;
           
            
        }
        if (Z != 0 && transform.localEulerAngles.z < LL)
        {
            isClock = true;
     
        }
    }
}

