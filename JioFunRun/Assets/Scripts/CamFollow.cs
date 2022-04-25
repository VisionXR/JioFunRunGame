using System.Collections;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public static CamFollow instance;
    public GameObject JMR;
    public Vector3 offset1,offset2,offset3,offset4,offset5;
    public Vector3 Rotoffset1,Rotoffset2,Rotoffset3,Rotoffset4,Rotoffset5;
    int i = 1;
    public float RotSpeed;
    public float LerpTime = 0.5f;
    float angle = 0;
    float t = 0;

    private void Awake()
    {
        instance = this;
    }
    public void Reset()
    {
       i = 1;
    }
    void LateUpdate()
    {
        if (t < LerpTime)
        {
            t += Time.deltaTime;
        }
      
        if (i == 1)
        {

            transform.position = Vector3.Lerp(transform.position,JMR.transform.position + offset1, t/LerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotoffset1), t / LerpTime);
         //   transform.eulerAngles  = Rotoffset1;
            
        }
        else if (i == 2)
        {
            transform.position = Vector3.Lerp(transform.position, JMR.transform.position + offset2, t / LerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotoffset2), t / LerpTime);
           // transform.eulerAngles = Rotoffset2;
        }
        else if (i == 3)
        {
            transform.position = Vector3.Lerp(transform.position, JMR.transform.position + offset3, t / LerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotoffset3), t / LerpTime);
            //transform.eulerAngles = Rotoffset3;
        }
        else if (i == 4)
        {
            transform.position = Vector3.Lerp(transform.position, JMR.transform.position + offset4, t / LerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotoffset4), t / LerpTime);
           // transform.eulerAngles = Rotoffset4;
        }
        else if (i == 5)
        {
            transform.position = Vector3.Lerp(transform.position, JMR.transform.position + offset5, t / LerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotoffset5), t / LerpTime);
          //  transform.eulerAngles = Rotoffset5;
        }
        else if(i == 6)
        {
            angle = Time.deltaTime * RotSpeed;
            transform.RotateAround(JMR.transform.position, Vector3.up, angle);
            StartCoroutine(CamRevolution());
        }
        
    }

    public void ChangeOffset(int i)
    {
        this.i = i;
        t = 0;
    }

    private IEnumerator CamRevolution()
    {
        yield return new WaitForSeconds(12);
        i = 1;
    }
}
