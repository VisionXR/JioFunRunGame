using UnityEngine;

public class OffsetTriggerChange : MonoBehaviour
{
    public int NewTrigger;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CamFollow.instance.ChangeOffset(NewTrigger);
        }
    }
}
