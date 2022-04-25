using UnityEngine;

public class LocalRot : MonoBehaviour
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
            transform.localEulerAngles = new Vector3(X * angle, Y * angle, Z * angle);
        }
        else
        {
            angle -= Speed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(X * angle, Y * angle, Z * angle);
        }
        if(angle > 360)
        {
            angle -= 360;
        }
        if(angle < 0)
        {
            angle += 360;
        }
    }
}

