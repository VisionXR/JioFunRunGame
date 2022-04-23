using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque2 : MonoBehaviour
{
    public float Force;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce((transform.forward -transform.up).normalized* Force);
    }
}
