using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotWheel : MonoBehaviour
{
    Rigidbody rb;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude <= 0.5)
        {
            transform.eulerAngles = Vector3.zero;
            if (transform.position.z > 0)
            {
                rb.AddForce(-1*transform.up * Force, ForceMode.VelocityChange);
            }
            else if (transform.position.z < 0)
            {
                rb.AddForce(1*transform.up * Force, ForceMode.VelocityChange);
            }
        }
    }
}
