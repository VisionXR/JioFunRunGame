using UnityEngine;

public class HorizontalSHM : MonoBehaviour
{
    public float Speed;
    public float X, Y, Z;
    public float LL, UL;
    public bool isClock = true;

    void Update()
    {
        if(isClock)
        {
            transform.localPosition += new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        else
        {
            transform.localPosition -= new Vector3(X, Y, Z) * Speed * Time.deltaTime;
        }
        if(X!= 0 && transform.localPosition.x > UL)
        {
            isClock = false;
        }
        if (X != 0 && transform.localPosition.x < LL)
        {
            isClock = true;
        }
        if (Y != 0 && transform.localPosition.y > UL)
        {
            isClock = false;
        }
        if (Y != 0 && transform.localPosition.y < LL)
        {
            isClock = true;
        }
        if (Z != 0 && transform.localPosition.z > UL)
        {
            isClock = false;
        }
        if (Z != 0 && transform.localPosition.z < LL)
        {
            isClock = true;
        }
    }
}
