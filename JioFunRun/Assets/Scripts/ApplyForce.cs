using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public Rigidbody rb;
    public float Force;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude <= 1)
        {
            transform.eulerAngles = Vector3.zero;
            rb.AddForce(transform.right * Force, ForceMode.Impulse);
        }
    }
}
