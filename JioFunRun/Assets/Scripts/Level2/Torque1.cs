using System.Collections;
using UnityEngine;

public class Torque1 : MonoBehaviour
{

    private Vector3 initPos, initRot;
    public float t = 1;
    public Rigidbody rb;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        initRot = transform.eulerAngles;
        StartCoroutine(ApplyTorque());

    }

    private IEnumerator ApplyTorque()
    {
        while (true)
        {
            yield return new WaitForSeconds(t);
              rb.velocity = Vector3.zero;
              rb.angularVelocity = Vector3.zero;
              transform.position = initPos;
              transform.eulerAngles = initRot;
         

        }
    }
    private void Update()
    {
          
    }
}
