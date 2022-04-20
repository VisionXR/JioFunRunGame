
using UnityEngine;

public class SHMRotation1 : MonoBehaviour
{
    public float Speed;
    public float X, Y, Z;
    public float LL, UL;
    public bool isClock = true;
    private Vector3 angle = Vector3.zero;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isClock)
        {
            angle += new Vector3(X, Y, Z) * Speed * Time.deltaTime;
            transform.localEulerAngles = angle; 
        }
        else
        {
            angle -= new Vector3(X, Y, Z) * Speed * Time.deltaTime;
            transform.localEulerAngles = angle;
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

