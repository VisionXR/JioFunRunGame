using UnityEngine;

public class SHMGlobal : MonoBehaviour
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
            angle += Speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(X*angle, Y*angle, Z*angle);
        }
        else
        {
            angle -= Speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(X*angle, Y*angle, Z*angle);
        }
        if (X != 0 && angle> UL)
        {
            isClock = false;
        }
        if (X != 0 && angle < LL)
        {
            isClock = true;
        }
        if (Y != 0 && angle > UL)
        {
            isClock = false;
        }
        if (Y != 0 && angle < LL)
        {
            isClock = true;
        }
        if (Z != 0 && angle > UL)
        {
            isClock = false;
        }
        if (Z != 0 && angle < LL)
        {
            isClock = true;
        }
    }
}

