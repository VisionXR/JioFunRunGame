using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque2 : MonoBehaviour
{
    public float Force;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce((this.transform.forward -this.transform.up).normalized* Force);
        Debug.Log(" it came in to On trigger Enter");
    }
}
